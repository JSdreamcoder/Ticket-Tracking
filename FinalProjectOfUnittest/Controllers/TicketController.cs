﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProjectOfUnittest.Data;
using FinalProjectOfUnittest.Models;
using FinalProjectOfUnittest.Data.BLL;
using FinalProjectOfUnittest.Data.DAL;
using Assignment_QnAWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FinalProjectOfUnittest.Controllers
{
    public class TicketController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly TicketBLL ticketbll;
        private readonly AppUserBLL userbll;
        private readonly ProjectBLL projectbll;
        private readonly TicketAttachmentBLL ticketAttachmentBLL;
        private readonly ProjectUserBLL projectUserbll;
        private readonly TIcketCommentBLL ticketCommentbll;
        private readonly TicketHistoryBLL ticketHistoryBLL;
        private readonly TicketLogItemBLL ticketLogItemBLL;
        private readonly TicketNotificationBLL ticketNotificationbll;
        
        public TicketController(ApplicationDbContext context,UserManager<AppUser> um)
        {
            ticketbll = new TicketBLL(new TicketDAL(context));
            userbll = new AppUserBLL(new AppUserDAL(context));
            projectbll = new ProjectBLL(new ProjectDAL(context));
            ticketAttachmentBLL = new TicketAttachmentBLL(new TicketAttachmentDAL(context));
            projectUserbll = new ProjectUserBLL(new ProjectUserDAL(context));
            ticketCommentbll = new TIcketCommentBLL(new TicketCommentDAL(context));
            ticketHistoryBLL = new TicketHistoryBLL(new TicketHistoryDAL(context));
            ticketLogItemBLL = new TicketLogItemBLL(new TicketLogItemDAL(context));
            ticketNotificationbll = new TicketNotificationBLL(new TicketNotificationDAL(context));
            userManager = um;
        }
        
        // GET: Tickets
        public async Task<IActionResult> Index(int projectid,string projectname,string searchString, string currentFilter, int? pageNumber,TicketPriorities priority,TicketTypes type,TicketStatus status,DateTime fromdate,DateTime todate,SortbyList sortby,int numofitems)
        {
            
            var userName = User.Identity.Name;
            var user = new AppUser();
            if (userName != null)
              user = await userManager.FindByNameAsync(userName);
            ViewBag.UserRole = await userManager.GetRolesAsync(user);
            ViewBag.UserId = user.Id;
            ViewData["CurrentFilter"] = searchString;
            ViewBag.ProjectName = projectname;
            ViewBag.ProjectId = projectid;
            ViewBag.IsProjectUser = false;
            // all users who assinged this project
 //Unit test   - easy
            var projectUser = projectUserbll.GetAll().FirstOrDefault(pu=> pu.ProjectId ==projectid &&  pu.UserId == user.Id);
            if (projectUser != null)
                ViewBag.IsProjectUser = true;
            if (searchString != null || priority != TicketPriorities.TicketPriority || type != TicketTypes.TicketType || status != TicketStatus.TicketStatus)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            var Tickets = ticketbll.GetAll().Where(t=>t.ProjectId==projectid);
//Unit test - worthy try
            var TicketsWithAssinedUser = ticketbll.GetList(t=>t.AssignedToUserId != null);
            // For Searching
            if (!String.IsNullOrEmpty(searchString))
            {
                var testList = new List<Ticket>();
                var SortedByTitle = Tickets.Where(t=>t.Title.ToLower().Contains(searchString.ToLower())).ToList();

                var SortedByOwner = Tickets.Where(t => t.OwnerUser.UserName.ToLower().Contains(searchString.ToLower())).ToList();

                
                var SortedByAssignedUser= TicketsWithAssinedUser.Where(t => t.AssignedToUser.UserName.ToLower().Contains(searchString.ToLower())).ToList();


                Tickets = SortedByTitle.Concat(SortedByOwner)
                                         .Concat(SortedByAssignedUser)
                                         .ToList();
            }

            // For Filtering
            var defaultDate = new DateTime();
            if(priority != TicketPriorities.TicketPriority)
                Tickets = Tickets.Where(t=>t.TicketPriority == priority).ToList();
            if(status != TicketStatus.TicketStatus)
                Tickets = Tickets.Where(t=>t.TicketStatus ==status).ToList();
            if(type != TicketTypes.TicketType)
                Tickets = Tickets.Where(t=>t.TicketType ==type).ToList();
            if (fromdate != defaultDate)
                Tickets = Tickets.Where(t => t.Created >= fromdate);
            if(todate != defaultDate)
                Tickets = Tickets.Where(t=>t.Created <= todate);

            //for Sorting
            var sortbyList = new List<string> { "Submitter", "Newest Create", "Newest Update", "Ticket Type", "Ticket Priority", "Ticket Satus" };
            if (sortby == SortbyList.Submitter)
            {
                Tickets = Tickets.OrderByDescending(t => t.OwnerUser.UserName);
            }else if (sortby == SortbyList.NewestCreate)
            {
                Tickets = Tickets.OrderByDescending(t=>t.Created);
            }else if(sortby == SortbyList.NewestUpdate)
            {
                Tickets = Tickets.OrderByDescending(t=>t.Updated);
            }else if(sortby == SortbyList.TicketType)
            {
                Tickets = Tickets.OrderByDescending((t)=>t.TicketType);
            }else if(sortby == SortbyList.TicketPriority)
            {
                Tickets = Tickets.OrderBy((t)=>t.TicketPriority);
            }else if(sortby == SortbyList.TicketStatus)
            {
                Tickets = Tickets.OrderByDescending((t)=>t.TicketStatus);
            }else
            {
                Tickets = Tickets.OrderByDescending(t => t.Created);
            }

                // For paging
                int pageSize = numofitems;
            ViewBag.PageSize = pageSize;
            if (numofitems == 0)
                pageSize = 10;
            
            return View(PaginatedList<Ticket>.Create(Tickets,pageNumber ?? 1,pageSize));
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int id,int? noticeId)
        {
            

            if (id == null || ticketbll.GetAll() == null)
            {
                return NotFound();
            }

            if (noticeId != null)
            {
                var notice = ticketNotificationbll.Get(notice => notice.Id == noticeId);
                notice.IsOpen = true;
                ticketNotificationbll.Update(notice);
                ticketNotificationbll.Save();
            }
            //when visit link to detail from NoticeList page,need to update
            UpdateNoticeCount();

            var ticket = ticketbll.GetAll()
                .FirstOrDefault(m => m.Id == id);
            //var filePaths = new List<string>();
            //if(ticket.TicketAttachments != null)
            //{
            //    filePaths = ticket.TicketAttachments.Select(t => t.FilePath).ToList();
            //}
           
            //List<string> fileNames = new List<string>();
            //foreach (var file in ticket.TicketAttachments)
            //{
            //    fileNames.Add(file.FilePath);
            //}
           
            if (ticket == null)
            {
                return NotFound();
            }
            //var ticketAndfilePaths = new ViewModel(fileNames ,ticket);
            return View(ticket);
        }
        public IActionResult ViewHistory(int ticketid)
        {
            var ticket = ticketbll.GetById(ticketid);
            ViewBag.TicketName = ticket.Title;
            ViewBag.TicketId = ticketid;
            return View(ticketHistoryBLL.GetList(t=>t.TicketId==ticketid).ToList());
        }
        public IActionResult ViewTicketLog(int ticketlogid, int ticketid)
        {
            ViewBag.TicketId = ticketid;
            return View(ticketLogItemBLL.GetById(ticketlogid));
        }
        public FileResult Download(int attachmentId)
        {
            var attachment = ticketAttachmentBLL.GetById(attachmentId);
            byte[] fileBytes = attachment.file;
            string fileName = attachment.FilePath;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
     

        //public IActionResult UpLoad()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> UpLoad(List<IFormFile> files)
        //{
        //    string folederPath = Environment.CurrentDirectory + "\\UploadFiles\\";
        //    long size = files.Sum(f => f.Length);

        //    foreach (var formFile in files)
        //    {
        //        if (formFile.Length > 0)
        //        {
        //            var filePath = Path.Combine(folederPath,Path.GetFileName(formFile.FileName));

        //            using (var stream = System.IO.File.Create(filePath))
        //            {
        //                await formFile.CopyToAsync(stream);
        //            }
        //        }
        //    }

        // Process uploaded files
        // Don't rely on or trust the FileName property without validation.

        //    return Ok(new { count = files.Count, size });
        //}
        // GET: Tickets/Create
        public IActionResult Create(int projectid)
        {
            if (User.Identity.Name == null)
            {
                return RedirectToAction("Error", new {message = "You need to log in"});
            }
            
            ViewBag.ProjectId = projectid;
            var userName = User.Identity.Name;
            var userId = userbll.Get(u=> u.UserName == userName).Id;
            ViewBag.UserId = userId;
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Created,Updated,ProjectId,TicketType,TicketPriority,TicketStatus,OwnerUserId,AssignedToUserId")] Ticket ticket, List<IFormFile> files)
        {
            var project = projectbll.GetById(ticket.ProjectId);
            //GeneralQuestion,  // Low  - default value
            //BugReport,        // High
            //
            //Payment,          // Medium
            //TechIssue,        // Medium
            //AccountIssue      // High
            ticket = SetTicketPriority(ticket);

            if (ModelState.IsValid)
            {
                ticketbll.Add(ticket);
                ticketbll.Save();

                //for File upload and this is from Microsoft
                foreach (var formFile in files)
                {


                    if (formFile.Length>0 && formFile.Length <300000)
                    {

                        //Create TicketAttachment
                        var newAttachment = new TicketAttachment();
                        newAttachment.FilePath = formFile.FileName;
                        newAttachment.Created = DateTime.Now;
                        newAttachment.UserId = ticket.OwnerUserId;
                        newAttachment.TicketId = ticket.Id;
                        using (var target = new MemoryStream())
                        {
                            formFile.CopyTo(target);
                            newAttachment.file = target.ToArray();
                        }
                        ticketAttachmentBLL.Add(newAttachment);
                        ticketAttachmentBLL.Save();
                    }

                }
                //End of file upload code


                return RedirectToAction(nameof(Index), new { projectid = project.Id, projectname = project.Name });
            }
            return View(ticket);
        }
        [Authorize(Roles ="Administrator,ProjectManager")]
        public async Task<IActionResult> AssignTicketToUser(int ticketid,string? message,int dummy)
        {
            try
            {
                var allusers = userbll.GetAllUsers().ToList();
                var AllSubmmiters = await userManager.GetUsersInRoleAsync("Submitter");
                var AllDeveloper = await userManager.GetUsersInRoleAsync("Developer");
                var AllProjectManager = await userManager.GetUsersInRoleAsync("ProjectManager");
                var AllAdmins = await userManager.GetUsersInRoleAsync("Administrator");

                var ticket = ticketbll.GetById(ticketid);
                var assignedUser = new AppUser();
                if (ticket.AssignedToUser != null)
                {
                   assignedUser = userbll.GetUserbyId(ticket.AssignedToUserId);
                }
                 
                ViewBag.AssignedUserName = assignedUser.UserName;
                ViewBag.Ticket = ticket;
                ViewBag.Message = message;
                ViewBag.ProjectId = ticket.ProjectId;
                // prevent for selectList of users from having the users that ticket aleady assigned
                var loginedUserName = User.Identity.Name;
                var loginedUser = userbll.Get(u=>u.UserName == loginedUserName);
                // divide Seleclist users depends on User Role
                var SelectListUsers = new List<AppUser>();
                if (User.IsInRole("Administrator"))
                {
                    SelectListUsers = allusers.Except(AllSubmmiters).Where(u => u.Id != assignedUser.Id).ToList();

                }
                else if (User.IsInRole("ProjectManager"))
                {
                    SelectListUsers = AllDeveloper.Where(u=>u.Id != assignedUser.Id).ToList(); 
                }

                var guest = userbll.GetUserbyId("df8353b4-9f11-4369-b2be-49725abb56d3");
                if(guest.Id!=assignedUser.Id) SelectListUsers.Add(guest);
                    var selectlistOfUsers = new SelectList(SelectListUsers, "Id", "UserName");
               

                return View(selectlistOfUsers);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost] // HTTP POST 
        public async Task<IActionResult> AssignTicketToUser(int ticketid, string userid)
        {
            try
            {
                
                //Getting Ticket
                var ticket = ticketbll.GetById(ticketid);

                //Getting User
                var LoginedUserName = User.Identity.Name;
                var LoginedUser = userbll.Get(u => u.UserName == LoginedUserName);

                //Getting Time now
                var NowTime = DateTime.Now;

                //Making Ticket History
                var ticketHistory = new TicketHistory();
                ticketHistory.Property = $"AssignedToUserId({userid}),Updated({NowTime}),TicketStatus({TicketStatus.Assigned})";
                ticketHistory.Changed = NowTime; // .Value makes equal corecctly between nullable and non nullable 
                ticketHistory.UserId = LoginedUser.Id;
                ticketHistory.TicketId = ticketid;
                ticketHistory.Changed = NowTime;
                ticketHistoryBLL.Add(ticketHistory);
                ticketHistoryBLL.Save();

                //Making TicketLogItem
                var ticketLogItem = new TicketLogItem();
                ticketLogItem.TicketHistoryId = ticketHistory.Id;
                ticketLogItem.Title = ticket.Title;
                ticketLogItem.Description = ticket.Description;
                ticketLogItem.Created = ticket.Created;
                ticketLogItem.Updated = NowTime;
                ticketLogItem.ProjectId = ticket.ProjectId;
                ticketLogItem.TicketType = ticket.TicketType;
                ticketLogItem.OwnerUserId = ticket.OwnerUserId;
                ticketLogItem.AssignedToUserId = ticket.AssignedToUserId;
                ticketLogItemBLL.Add(ticketLogItem);
                ticketLogItemBLL.Save();

                //Making ticket
                ticket.AssignedToUserId = userid;
                ticket.Updated = NowTime;
                ticket.TicketStatus = TicketStatus.Assigned;
                ticketbll.Update(ticket);
                ticketbll.Save();

                //Making TicketNotification
                var newNotification = new TicketNotification();
                newNotification.TicketId = ticket.Id;
                newNotification.UserId = userid;
                newNotification.IsOpen = false;
                ticketNotificationbll.Add(newNotification);
                ticketNotificationbll.Save();

                //Update Notice Count
                UpdateNoticeCount();
                

                


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction("AssignTicketToUser", new {ticketid = ticketid, message = "Success"});
        }
        [HttpPost]
        public async Task<IActionResult> CompleteTicket(int ticketid, int noticeId)
        {
            //Change IsOpen to True
            var notice = ticketNotificationbll.Get(notice => noticeId == notice.Id);
            notice.IsOpen = true;
            ticketNotificationbll.Update(notice);
            ticketNotificationbll.Save();
            UpdateNoticeCount();
            //Get Ticket
            var ticket = ticketbll.GetById(ticketid);

            //Getting User
            var LoginedUserName = User.Identity.Name;
            var LoginedUser = userbll.Get(u => u.UserName == LoginedUserName);

            //Getting Time now
            var NowTime = DateTime.Now;

            //Making Ticket History
            var ticketHistory = new TicketHistory();
            ticketHistory.Property = $"Updated({NowTime}),TicketStatus({TicketStatus.Completed})";
            ticketHistory.Changed = NowTime; // .Value makes equal corecctly between nullable and non nullable 
            ticketHistory.UserId = LoginedUser.Id;
            ticketHistory.TicketId = ticketid;
            ticketHistoryBLL.Add(ticketHistory);
            ticketHistoryBLL.Save();

            //Making TicketLogItem
            var ticketLogItem = new TicketLogItem();
            ticketLogItem.TicketHistoryId = ticketHistory.Id;
            ticketLogItem.Title = ticket.Title;
            ticketLogItem.Description = ticket.Description;
            ticketLogItem.Created = ticket.Created;
            ticketLogItem.Updated = NowTime;
            ticketLogItem.ProjectId = ticket.ProjectId;
            ticketLogItem.TicketType = ticket.TicketType;
            ticketLogItem.OwnerUserId = ticket.OwnerUserId;
            ticketLogItem.AssignedToUserId = ticket.AssignedToUserId;
            ticketLogItemBLL.Add(ticketLogItem);
            ticketLogItemBLL.Save();

            //complete and Update ticket 
            ticket.TicketStatus = TicketStatus.Completed;
            ticketbll.Update(ticket);
            ticketbll.Save();

            //Getting project manager
            var projectManager = new AppUser();
            foreach (var projectuser in ticket.Project.ProjectUsers)
            {
                bool isProjectManager = await userManager.IsInRoleAsync(projectuser.User, "ProjectManager");
                if (isProjectManager)
                {
                    projectManager = projectuser.User;
                    //Maing TicketNotification
                    var newNotification = new TicketNotification();
                    newNotification.TicketId = ticket.Id;
                    newNotification.UserId = projectManager.Id;
                    newNotification.IsOpen = false;
                    ticketNotificationbll.Add(newNotification);
                    ticketNotificationbll.Save();
                    
                }
                   
            }
            return RedirectToAction("Index", "TicketNotifications");

        }



        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int id,string? message)
        {
            ViewBag.Message = message;
            if (id == null || ticketbll.GetAll() == null)
            {
                return NotFound();
            }

            var ticket = ticketbll.GetById(id);
            ViewBag.ProjectId = ticket.ProjectId;
            if (ticket == null)
            {
                return NotFound();
            }
           
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int ticketid, [Bind("Id,Title,Description,TicketType")] Ticket ticketinfo)
        {
            string changedpropety = "";
            var ticket = ticketbll.GetById(ticketinfo.Id);
            if (ticket == null)
            {
                return NotFound();
            }
            if(ticketinfo.Title != ticket.Title)
                changedpropety = $"Title({ticketinfo.Title}";
            if (ticketinfo.Description != ticket.Description)
                changedpropety = changedpropety + $",Desciption({ticketinfo.Description})";
            if (ticketinfo.TicketType != ticket.TicketType)
            {
                changedpropety = changedpropety + $",TicketType({ticketinfo.TicketType})";
            }

           
            try
            {
                //Getting User
                var LoginedUserName = User.Identity.Name;
                var LoginedUser = userbll.Get(u => u.UserName == LoginedUserName);

                //Getting Time now
                var NowTime = DateTime.Now;

                //Making Ticket History
                var ticketHistory = new TicketHistory();
                ticketHistory.Property = changedpropety + $",Updated({NowTime})";
                ticketHistory.Changed = NowTime; // .Value makes equal corecctly between nullable and non nullable 
                ticketHistory.UserId = LoginedUser.Id;
                ticketHistory.TicketId = ticketinfo.Id;
                ticketHistoryBLL.Add(ticketHistory);
                ticketHistoryBLL.Save();

                //Making TicketLogItem
                var ticketLogItem = new TicketLogItem();
                ticketLogItem.TicketHistoryId = ticketHistory.Id;
                ticketLogItem.Title = ticket.Title;
                ticketLogItem.Description = ticket.Description;
                ticketLogItem.Created = ticket.Created;
                ticketLogItem.Updated = NowTime;
                ticketLogItem.ProjectId = ticket.ProjectId;
                ticketLogItem.TicketType = ticket.TicketType;
                ticketLogItem.TicketStatus = ticket.TicketStatus;
                ticketLogItem.TicketPriority = ticket.TicketPriority;
                ticketLogItem.OwnerUserId = ticket.OwnerUserId;
                ticketLogItem.AssignedToUserId = ticket.AssignedToUserId;
                ticketLogItemBLL.Add(ticketLogItem);
                ticketLogItemBLL.Save();


                ticket.Title = ticketinfo.Title;
                ticket.Description = ticketinfo.Description;
                ticket.TicketType = ticketinfo.TicketType;
                ticket.Updated = DateTime.Now;
                ticket = SetTicketPriority(ticket);
                ticketbll.Update(ticket);
                ticketbll.Save();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return RedirectToAction(nameof(Edit), new {id = ticket.Id,message = "Success"});
            
          
            
        }
       
        //// GET: Tickets/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Ticket == null)
        //    {
        //        return NotFound();
        //    }

        //    var ticket = await _context.Ticket
        //        .Include(t => t.AssignedToUser)
        //        .Include(t => t.OwnerUser)
        //        .Include(t => t.Project)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (ticket == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(ticket);
        //}

        //// POST: Tickets/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Ticket == null)
        //    {
        //        return Problem("Entity set 'ApplicationDbContext.Ticket'  is null.");
        //    }
        //    var ticket = await _context.Ticket.FindAsync(id);
        //    if (ticket != null)
        //    {
        //        _context.Ticket.Remove(ticket);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool TicketExists(int id)
        //{
        //  return (_context.Ticket?.Any(e => e.Id == id)).GetValueOrDefault();
        //}

        public IActionResult Error(string message)
        {
            ViewBag.Message = message;
            return View();
        }

        public Ticket SetTicketPriority(Ticket ticket)
        {
            //GeneralQuestion,  // Low  - default value
            //BugReport,        // High
            //Payment,          // Medium
            //TechIssue,        // Medium
            //AccountIssue      // High
            switch (ticket.TicketType)
            {
                case TicketTypes.BugReport:
                    ticket.TicketPriority = TicketPriorities.High;
                    break;
                case TicketTypes.Payment:
                    ticket.TicketPriority = TicketPriorities.Medium;
                    break;
                case TicketTypes.TechIssue:
                    ticket.TicketPriority = TicketPriorities.Medium;
                    break;
                case TicketTypes.AccountIssue:
                    ticket.TicketPriority = TicketPriorities.High;
                    break;
                case TicketTypes.GeneralQuestion:
                    ticket.TicketPriority = TicketPriorities.Low;
                    break;
            }

            return ticket;
        }

        public void UpdateNoticeCount()
        {
            var userName = User.Identity.Name;
            if (userName != null)
            {
                var user = userbll.Get(u => u.UserName == userName);
                var userId = user.Id;

                var noticesByUser = ticketNotificationbll.GetList(n => n.UserId == userId && n.IsOpen == false);

                GlobalValue.NoticeCount = noticesByUser.Count;

            }
        }
    }
}
