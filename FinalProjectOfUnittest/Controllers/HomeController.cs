using FinalProjectOfUnittest.Data;
using FinalProjectOfUnittest.Data.BLL;
using FinalProjectOfUnittest.Data.DAL;
using FinalProjectOfUnittest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Diagnostics;
using System;
using System.Threading.Tasks;


namespace FinalProjectOfUnittest.Controllers
{
    public class HomeController : Controller
    {

        public ProjectBLL projectBLL;
        public TicketNotificationBLL noticebll;
        public AppUserBLL appUserBLL;
        public UserManager<AppUser> userManager;
        
        public HomeController(ApplicationDbContext context, UserManager<AppUser> um)
        {
            projectBLL = new ProjectBLL(new ProjectDAL(context));
            noticebll = new TicketNotificationBLL(new TicketNotificationDAL(context));
            appUserBLL = new AppUserBLL(new AppUserDAL(context));
            userManager = um;
        }
      
       
        public async Task<IActionResult> Index()
        {
            UpdateNoticeCount();
            return View(projectBLL.GetAll().ToList());
        }

        public void UpdateNoticeCount()
        {
            var userName = User.Identity.Name;
            if (userName != null)
            {
                var user = appUserBLL.Get(u => u.UserName == userName);
                var userId = user.Id;

                var noticesByUser = noticebll.GetList(n => n.UserId == userId && n.IsOpen == false);

                GlobalValue.NoticeCount = noticesByUser.Count;

            }
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