#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProjectOfUnittest.Data;
using FinalProjectOfUnittest.Models;

namespace FinalProjectOfUnittest.Controllers
{
    public class TicketNotificationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketNotificationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TicketNotifications
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TicketNotification.Include(t => t.Ticket).Include(t => t.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TicketNotifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketNotification = await _context.TicketNotification
                .Include(t => t.Ticket)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketNotification == null)
            {
                return NotFound();
            }

            return View(ticketNotification);
        }

        // GET: TicketNotifications/Create
        public IActionResult Create()
        {
            ViewData["TicketId"] = new SelectList(_context.Ticket, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }

        // POST: TicketNotifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,TicketId")] TicketNotification ticketNotification)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticketNotification);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TicketId"] = new SelectList(_context.Ticket, "Id", "Id", ticketNotification.TicketId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", ticketNotification.UserId);
            return View(ticketNotification);
        }

        // GET: TicketNotifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketNotification = await _context.TicketNotification.FindAsync(id);
            if (ticketNotification == null)
            {
                return NotFound();
            }
            ViewData["TicketId"] = new SelectList(_context.Ticket, "Id", "Id", ticketNotification.TicketId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", ticketNotification.UserId);
            return View(ticketNotification);
        }

        // POST: TicketNotifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,TicketId")] TicketNotification ticketNotification)
        {
            if (id != ticketNotification.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticketNotification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketNotificationExists(ticketNotification.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TicketId"] = new SelectList(_context.Ticket, "Id", "Id", ticketNotification.TicketId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", ticketNotification.UserId);
            return View(ticketNotification);
        }

        // GET: TicketNotifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketNotification = await _context.TicketNotification
                .Include(t => t.Ticket)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketNotification == null)
            {
                return NotFound();
            }

            return View(ticketNotification);
        }

        // POST: TicketNotifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticketNotification = await _context.TicketNotification.FindAsync(id);
            _context.TicketNotification.Remove(ticketNotification);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketNotificationExists(int id)
        {
            return _context.TicketNotification.Any(e => e.Id == id);
        }
    }
}
