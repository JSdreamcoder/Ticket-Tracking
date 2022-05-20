using System;
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

namespace FinalProjectOfUnittest.Controllers
{
    public class TicketController : Controller
    {
        private readonly TicketBLL ticketbll;
        private readonly UserManager<AppUser> userManager;  
        private readonly AppUserBLL userbll;
        private readonly ProjectBLL projectbll;
        private readonly TicketAttachmentBLL ticketAttachmentBLL;
        public TicketController(ApplicationDbContext context,UserManager<AppUser> um)
        {
            ticketbll = new TicketBLL(new TicketDAL(context));
            userbll = new AppUserBLL(new AppUserDAL(context));
            projectbll = new ProjectBLL(new ProjectDAL(context));
            ticketAttachmentBLL = new TicketAttachmentBLL(new TicketAttachmentDAL(context));
            userManager = um;
        }

        // GET: Tickets
        public async Task<IActionResult> Index(int projectid,string projectname,string searchString, string currentFilter, int? pageNumber)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewBag.ProjectName = projectname;
            ViewBag.projectId = projectid;
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            var Tickets = ticketbll.GetAll().Where(t=>t.ProjectId==projectid);

            
            if (!String.IsNullOrEmpty(searchString))
            {
               Tickets = Tickets.Where(t=>t.Title.ToLower().Contains(searchString.ToLower())).ToList();
            }
            int pageSize = 10;
            return View(PaginatedList<Ticket>.Create(Tickets,pageNumber ?? 1,pageSize));
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || ticketbll.GetAll() == null)
            {
                return NotFound();
            }

            var ticket = ticketbll.GetAll()
                .FirstOrDefault(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
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
            //Payment,          // Medium
            //TechIssue,        // Medium
            //AccountIssue      // High
            switch (ticket.TicketType)
            {
                case TicketTypes.BugReport :
                    ticket.TicketPriority = TicketPriorities.High;
                    break;
                case TicketTypes.Payment:
                    ticket.TicketPriority = TicketPriorities.Medium;
                    break;
                case TicketTypes.TechIssue :
                    ticket.TicketPriority = TicketPriorities.Medium;
                    break ;
                case TicketTypes.AccountIssue :
                    ticket.TicketPriority = TicketPriorities.High;
                    break ;
            }

            if (ModelState.IsValid)
            {
                ticketbll.Add(ticket);
                ticketbll.Save();

                //for File upload and thi is from Microsoft
                foreach (var formFile in files)
                {
                    string folederPath = Environment.CurrentDirectory + "\\UploadFiles\\";
                    string filePath = "";
                    if (formFile.Length > 0)
                    {
                        filePath = Path.Combine(folederPath, Path.GetFileName(formFile.FileName));

                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                    }

                    //Create TicketAttachment
                    var newAttachment = new TicketAttachment();
                    newAttachment.FilePath = filePath;
                    newAttachment.Created = DateTime.Now;
                    newAttachment.UserId = ticket.OwnerUserId;
                    newAttachment.TicketId = ticket.Id;
                    ticketAttachmentBLL.Add(newAttachment);
                    ticketAttachmentBLL.Save();
                }
                //End of file upload code


                return RedirectToAction(nameof(Index), new { projectid = project.Id, projectname = project.Name });
            }

            

            
           
        


            return View(ticket);
        }

        //// GET: Tickets/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Ticket == null)
        //    {
        //        return NotFound();
        //    }

        //    var ticket = await _context.Ticket.FindAsync(id);
        //    if (ticket == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["AssignedToUserId"] = new SelectList(_context.AppUser, "Id", "Id", ticket.AssignedToUserId);
        //    ViewData["OwnerUserId"] = new SelectList(_context.AppUser, "Id", "Id", ticket.OwnerUserId);
        //    ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Id", ticket.ProjectId);
        //    return View(ticket);
        //}

        //// POST: Tickets/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Created,Updated,ProjectId,TicketType,TicketPriority,TicketStatus,OwnerUserId,AssignedToUserId")] Ticket ticket)
        //{
        //    if (id != ticket.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(ticket);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TicketExists(ticket.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["AssignedToUserId"] = new SelectList(_context.AppUser, "Id", "Id", ticket.AssignedToUserId);
        //    ViewData["OwnerUserId"] = new SelectList(_context.AppUser, "Id", "Id", ticket.OwnerUserId);
        //    ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Id", ticket.ProjectId);
        //    return View(ticket);
        //}

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
    }
}
