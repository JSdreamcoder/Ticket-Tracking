using FinalProjectOfUnittest.Areas.Identity.Pages.Account;
using FinalProjectOfUnittest.Data;
using FinalProjectOfUnittest.Data.BLL;
using FinalProjectOfUnittest.Data.DAL;
using FinalProjectOfUnittest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace FinalProjectOfUnittest.Controllers
{
    public class AccountController : Controller
    {
        AppUserBLL userbll;
        UserManager<AppUser> userManager;
        public AccountController(ApplicationDbContext db,UserManager<AppUser>um)
        {
            userbll = new AppUserBLL(new AppUserDAL(db));
            userManager = um;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SendEmail(string email, string confirmurl)
        {
            var user = userbll.Get(u=>u.UserName == email);
            await userManager.AddToRoleAsync(user,"Submitter");
            userbll.Update(user);
            userbll.Save();
            var apiKey = Environment.GetEnvironmentVariable("jaewonhw");
            var client = new SendGridClient("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");
            var from = new EmailAddress("JBZgroup@email.com", "TicketSupportCenter");
            var subject = "Confirm Email from JBZ";
            var to = new EmailAddress(email, "Example User");
            var plainTextContent = "This is test email sender";
            var htmlContent = $"<strong>Once Click this Link <a id=\"confirm - link\" href={confirmurl}>Confirm Email</a> , your Sing up will be Completed </strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            return View();
            //Console.WriteLine(response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!");

        }

        public async Task<IActionResult> SendEmailForRecoverPassWord(string email)
        {

            ViewBag.Message = "Please Check your Email for Complete Recover Password";
            var user = userbll.Get(u=>u.Email==email);
            if (email == null)
            {
                ViewBag.Message = "Please input email";
                return View();
            }else if (user == null)
            {
                ViewBag.Message = "Can not find Id";
                return View();
            }
            var url = $"https://localhost:7211/Account/RecoverPassword?email={email}";
            var apiKey = Environment.GetEnvironmentVariable("jaewonhw");
            var client = new SendGridClient("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");
            var from = new EmailAddress("JBZgroup@email.com", "TicketSupportCenter");
            var subject = "Recover Password of JBZ";
            var to = new EmailAddress(email, "Example User");
            var plainTextContent = "This is test email sender";
            var htmlContent = $"<strong>Go to recover Pass word : <a href=\"{url}\">Recover Password</a></strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            return View();
            //Console.WriteLine(response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!");

        }

        public IActionResult RecoverPassword(string email, string message,string success)
        {
            ViewBag.Message = message;
            ViewBag.Success = success;
            var user = userbll.Get(u => u.Email == email);
            return View(user);
        }
        [HttpPost]
        public IActionResult RecoverPassword(string email, string password, string confirmpassword,int dummy)
        {
            if (password != confirmpassword)
            {
                return RedirectToAction("RecoverPassword", new { message = "confirm Password is different" , email = email });
            }

            var user = userbll.Get(u => u.Email == email);
            var passwordHasher = new PasswordHasher<AppUser>();
            var hashedPassword = passwordHasher.HashPassword(user, password);
            user.PasswordHash = hashedPassword;
            userbll.Update(user);
            userbll.Save();
            return RedirectToAction("RecoverPassword", new { success = "Password change is Completed" ,email = email });

        }
    }
}
