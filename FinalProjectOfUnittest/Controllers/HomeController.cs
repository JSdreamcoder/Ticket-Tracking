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
        public HomeController(ApplicationDbContext context)
        {
            projectBLL = new ProjectBLL(new ProjectDAL(context));
        }
       
        public IActionResult Index()
        {
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