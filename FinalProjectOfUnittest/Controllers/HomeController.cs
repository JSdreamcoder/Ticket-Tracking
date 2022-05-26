using FinalProjectOfUnittest.Data;
using FinalProjectOfUnittest.Data.BLL;
using FinalProjectOfUnittest.Data.DAL;
using FinalProjectOfUnittest.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace FinalProjectOfUnittest.Controllers
{
    public class HomeController : Controller
    {

        public ProjectBLL projectBLL;
        public TicketNotificationBLL noticebll;
        public AppUserBLL appUserBLL;
        public HomeController(ApplicationDbContext context)
        {
            projectBLL = new ProjectBLL(new ProjectDAL(context));
            noticebll = new TicketNotificationBLL(new TicketNotificationDAL(context));
            appUserBLL = new AppUserBLL(new AppUserDAL(context));
        }
      
        public IActionResult Index()
        {
            var userName = User.Identity.Name;
            if (userName != null)
            {
               var userId = appUserBLL.Get(u=>u.UserName == userName).Id;
               var noticesByUser = noticebll.GetList(n=>n.UserId==userId && n.Ticket.TicketStatus != TicketStatus.Completed);
               ViewBag.NumOfNotices = noticesByUser.Count;

            }
            
            
            return View(projectBLL.GetAll());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}