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


         public async Task<IActionResult> email()
        {
            var apiKey = Environment.GetEnvironmentVariable("SendGrid");
            var client = new SendGridClient("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");
            var from = new EmailAddress("test@example.com", "Example User");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("test@example.com", "Example User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            return Ok("Success");
        }
    }



    internal class Example
    {
        private static void Main()
        {
            Execute().Wait();
        }

        static async Task Execute()
        {
            var apiKey = Environment.GetEnvironmentVariable("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");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("test@example.com", "Example User");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("assess81@gmail.com", "Example User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

           
        }
    }



}