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

namespace FinalProjectOfUnittest.Controllers
{
    public class TicketController : Controller
    {
        private readonly TicketBLL ticketbll;

        public TicketController(ApplicationDbContext context)
        {
            ticketbll = new TicketBLL(new TicketDAL(context));
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

        //// GET: Tickets/Create
        //public IActionResult Create()
        //{
        //    ViewData["AssignedToUserId"] = new SelectList(_context.AppUser, "Id", "Id");
        //    ViewData["OwnerUserId"] = new SelectList(_context.AppUser, "Id", "Id");
        //    ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Id");
        //    return View();
        //}

        //// POST: Tickets/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Title,Description,Created,Updated,ProjectId,TicketType,TicketPriority,TicketStatus,OwnerUserId,AssignedToUserId")] Ticket ticket)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(ticket);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["AssignedToUserId"] = new SelectList(_context.AppUser, "Id", "Id", ticket.AssignedToUserId);
        //    ViewData["OwnerUserId"] = new SelectList(_context.AppUser, "Id", "Id", ticket.OwnerUserId);
        //    ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Id", ticket.ProjectId);
        //    return View(ticket);
        //}

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
    }
}
