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

namespace FinalProjectOfUnittest.Controllers
{
    public class TicketNotificationsController : Controller
    {
        private readonly TicketNotificationBLL noticebll;
        private readonly AppUserBLL userbll;

        public TicketNotificationsController(ApplicationDbContext context)
        {
            noticebll = new TicketNotificationBLL(new TicketNotificationDAL(context));
            userbll = new AppUserBLL(new AppUserDAL(context));
        }

        // GET: TicketNotifications
        public async Task<IActionResult> Index()
        {
            var userName = User.Identity.Name;
            var user = userbll.Get(u=>u.UserName == userName);
            var userid = user.Id;
            var allnotifications = noticebll.GetList(n => n.UserId == userid);
            return View(allnotifications.ToList());
        }

    //    // GET: TicketNotifications/Details/5
    //    public async Task<IActionResult> Details(int? id)
    //    {
    //        if (id == null || _context.TicketNotification == null)
    //        {
    //            return NotFound();
    //        }

    //        var ticketNotification = await _context.TicketNotification
    //            .Include(t => t.Ticket)
    //            .Include(t => t.User)
    //            .FirstOrDefaultAsync(m => m.Id == id);
    //        if (ticketNotification == null)
    //        {
    //            return NotFound();
    //        }

    //        return View(ticketNotification);
    //    }

    //    // GET: TicketNotifications/Create
    //    public IActionResult Create()
    //    {
    //        ViewData["TicketId"] = new SelectList(_context.Ticket, "Id", "Id");
    //        ViewData["UserId"] = new SelectList(_context.AppUser, "Id", "Id");
    //        return View();
    //    }

    //    // POST: TicketNotifications/Create
    //    // To protect from overposting attacks, enable the specific properties you want to bind to.
    //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Create([Bind("Id,UserId,TicketId")] TicketNotification ticketNotification)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            _context.Add(ticketNotification);
    //            await _context.SaveChangesAsync();
    //            return RedirectToAction(nameof(Index));
    //        }
    //        ViewData["TicketId"] = new SelectList(_context.Ticket, "Id", "Id", ticketNotification.TicketId);
    //        ViewData["UserId"] = new SelectList(_context.AppUser, "Id", "Id", ticketNotification.UserId);
    //        return View(ticketNotification);
    //    }

    //    // GET: TicketNotifications/Edit/5
    //    public async Task<IActionResult> Edit(int? id)
    //    {
    //        if (id == null || _context.TicketNotification == null)
    //        {
    //            return NotFound();
    //        }

    //        var ticketNotification = await _context.TicketNotification.FindAsync(id);
    //        if (ticketNotification == null)
    //        {
    //            return NotFound();
    //        }
    //        ViewData["TicketId"] = new SelectList(_context.Ticket, "Id", "Id", ticketNotification.TicketId);
    //        ViewData["UserId"] = new SelectList(_context.AppUser, "Id", "Id", ticketNotification.UserId);
    //        return View(ticketNotification);
    //    }

    //    // POST: TicketNotifications/Edit/5
    //    // To protect from overposting attacks, enable the specific properties you want to bind to.
    //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,TicketId")] TicketNotification ticketNotification)
    //    {
    //        if (id != ticketNotification.Id)
    //        {
    //            return NotFound();
    //        }

    //        if (ModelState.IsValid)
    //        {
    //            try
    //            {
    //                _context.Update(ticketNotification);
    //                await _context.SaveChangesAsync();
    //            }
    //            catch (DbUpdateConcurrencyException)
    //            {
    //                if (!TicketNotificationExists(ticketNotification.Id))
    //                {
    //                    return NotFound();
    //                }
    //                else
    //                {
    //                    throw;
    //                }
    //            }
    //            return RedirectToAction(nameof(Index));
    //        }
    //        ViewData["TicketId"] = new SelectList(_context.Ticket, "Id", "Id", ticketNotification.TicketId);
    //        ViewData["UserId"] = new SelectList(_context.AppUser, "Id", "Id", ticketNotification.UserId);
    //        return View(ticketNotification);
    //    }

    //    // GET: TicketNotifications/Delete/5
    //    public async Task<IActionResult> Delete(int? id)
    //    {
    //        if (id == null || _context.TicketNotification == null)
    //        {
    //            return NotFound();
    //        }

    //        var ticketNotification = await _context.TicketNotification
    //            .Include(t => t.Ticket)
    //            .Include(t => t.User)
    //            .FirstOrDefaultAsync(m => m.Id == id);
    //        if (ticketNotification == null)
    //        {
    //            return NotFound();
    //        }

    //        return View(ticketNotification);
    //    }

    //    // POST: TicketNotifications/Delete/5
    //    [HttpPost, ActionName("Delete")]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> DeleteConfirmed(int id)
    //    {
    //        if (_context.TicketNotification == null)
    //        {
    //            return Problem("Entity set 'ApplicationDbContext.TicketNotification'  is null.");
    //        }
    //        var ticketNotification = await _context.TicketNotification.FindAsync(id);
    //        if (ticketNotification != null)
    //        {
    //            _context.TicketNotification.Remove(ticketNotification);
    //        }
            
    //        await _context.SaveChangesAsync();
    //        return RedirectToAction(nameof(Index));
    //    }

    //    private bool TicketNotificationExists(int id)
    //    {
    //      return (_context.TicketNotification?.Any(e => e.Id == id)).GetValueOrDefault();
    //    }
    }
}
