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
    public class TicketCommentsController : Controller
    {
        
        private readonly AppUserBLL userbll;
        private readonly TIcketCommentBLL commentbll;
        public TicketCommentsController(ApplicationDbContext context)
        {
            userbll = new AppUserBLL(new AppUserDAL(context));
            commentbll = new TIcketCommentBLL(new TicketCommentDAL(context));
        }

       




      

        // my own action
        [HttpPost]
        public IActionResult Create(int ticketid, string comment)
        {
            try
            {
                var userName = User.Identity.Name;
                var user = userbll.Get(u => u.UserName == userName);
                var newComment = new TicketComment();
                newComment.Comment = comment;
                newComment.TicketId = ticketid;
                newComment.UserId = user.Id;
                newComment.Created = DateTime.Now;
                commentbll.Add(newComment);
                commentbll.Save();
                return RedirectToAction("Details","Ticket", new { id = ticketid });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: TicketComments/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || commentbll.GetAll() == null)
            {
                return NotFound();
            }

            var ticketComment = commentbll.Get(id);
            if (ticketComment == null)
            {
                return NotFound();
            }
           
            return View(ticketComment);
        }

        // POST: TicketComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Comment,Created,UserId,TicketId")] TicketComment ticketComment)
        {
            if (id != ticketComment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    commentbll.Update(ticketComment);
                    commentbll.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketCommentExists(ticketComment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details","Ticket", new {id=ticketComment.TicketId});
            }
          
            return View(ticketComment);
        }

        
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || commentbll.GetAll() == null)
            {
                return NotFound();
            }

            //var ticketComment = await _context.TicketComment
            //    .Include(t => t.Ticket)
            //    .Include(t => t.User)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var ticketComment = commentbll.Get(id);
            if (ticketComment == null)
            {
                return NotFound();
            }

            return View(ticketComment);
        }

        // POST: TicketComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (commentbll.GetAll() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TicketComment'  is null.");
            }
            var ticketComment = commentbll.Get(id);
            if (ticketComment != null)
            {
                commentbll.Delete(ticketComment);
            }

            commentbll.Save();
            return RedirectToAction("Details", "Ticket", new { id = ticketComment.TicketId });
        }

        private bool TicketCommentExists(int id)
        {
            return (commentbll.GetAll()?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
