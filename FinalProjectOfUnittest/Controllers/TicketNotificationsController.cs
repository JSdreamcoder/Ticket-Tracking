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
            allnotifications = allnotifications.OrderBy(n => n.IsOpen).ToList();
            return View(allnotifications);
        }



       

        //GET: TicketNotifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || noticebll == null)
            {
                return NotFound();
            }

            var ticketNotification = noticebll.Get(nb=>nb.Id==id);
               
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
            if (noticebll == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TicketNotification'  is null.");
            }
            var ticketNotification = noticebll.Get(nb => nb.Id == id);
            if (ticketNotification != null)
            {
                noticebll.Remove(ticketNotification);
            }

            noticebll.Save();
            return RedirectToAction(nameof(Index));
        }

      
    }
}
