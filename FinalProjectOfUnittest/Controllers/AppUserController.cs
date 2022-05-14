using FinalProjectOfUnittest.Data.BLL;
using FinalProjectOfUnittest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProjectOfUnittest.Controllers
{
    public class AppUserController : Controller
    {
        private readonly AppUserBLL userbll;
        private readonly RoleBLL rolebll;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public AppUserController(AppUserBLL ubll,UserManager<AppUser> um,RoleManager<IdentityRole> rm,RoleBLL rbll)
        {
            userbll = ubll;
            rolebll = rbll;
            userManager = um;
            roleManager = rm;
        }
        // GET: AppUserController
        public async Task<IActionResult> Index()
        {
            try
            {
                var userAndRoles = new Dictionary<AppUser, IList<string>>();
                var users = userbll.GetAllUsers();
                foreach (var user in users)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    userAndRoles.Add(user, roles);
                }
                return View(userAndRoles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return View();
        }
        public async Task<IActionResult> AssignRoleToUser(string userid)
        {
            try
            {
                var user = userbll.GetUserbyId(userid);
                var userRoles = await userManager.GetRolesAsync(user);
                var allRoles = rolebll.GetAllRoles();
                // prevent selectList of roles have the roles that user aleady have
                var otherRoles = allRoles.Where(r=>!userRoles.Contains(r.ToString())).ToList();
                ViewBag.User = user;
                ViewBag.UserRoles = await userManager.GetRolesAsync(user);
                var selectListOfRoles = new SelectList(otherRoles, "Name", "Name");

                return View(selectListOfRoles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AssignRolesToUser(string role, string userid)
        {
            try
            {
                var user = userbll.GetUserbyId(userid);
                await userManager.AddToRoleAsync(user, role);
                await userManager.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction("Index");
        }
        // GET: AppUserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AppUserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppUserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AppUserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AppUserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AppUserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AppUserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
