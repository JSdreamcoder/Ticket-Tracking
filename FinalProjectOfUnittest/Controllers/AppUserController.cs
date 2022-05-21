using FinalProjectOfUnittest.Data;
using FinalProjectOfUnittest.Data.BLL;
using FinalProjectOfUnittest.Data.DAL;
using FinalProjectOfUnittest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProjectOfUnittest.Controllers
{
    [Authorize(Roles = "Adminitarater")]
    public class AppUserController : Controller
    {

        //make declaration first like last group project, difference is using appuserbll, bll gets data from dal
        private readonly ApplicationDbContext context;
        private readonly AppUserBLL userbll;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly RoleBLL rolebll;
        public AppUserController(ApplicationDbContext db,UserManager<AppUser> um, RoleManager<IdentityRole> rm)
        {
            userbll = new AppUserBLL(new AppUserDAL(db));
            userManager = um;
            roleManager = rm;
            rolebll = new RoleBLL(new RoleDAL(db));
        }
        // GET: AppUserController
        public async Task<IActionResult> Index()
        {
            try
            {
                var userAndRoles = new Dictionary<AppUser, IList<string>>();
                //using bll is like a GET, should have a function call all user list in BLL(appuserbll line19)
                var users = userbll.GetAllUsers(); 
                foreach (var user in users)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    //async await is like a step by step coding, system will check await part run or not
                    userAndRoles.Add(user, roles);//Dictionary should use key and value
                }
                return View(userAndRoles);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        
        //GET METHOD
        public async Task<IActionResult> AssignRoleToUser(string userid)
        {
            try
            {
                var user = userbll.GetUserbyId(userid);
                var userRoles = await userManager.GetRolesAsync(user);
                var allRoles = rolebll.GetAllRoles();//Need to use RoleDal

                // prevent selectList of roles have the roles that user aleady have
               //var otherRoless = allRoles.Where(r => !userRoles.Contains(r.ToString())).ToList();
                 var otherRoles = rolebll.GetCounterPartRoles(userRoles);

                ViewBag.User = user;
                ViewBag.UserRoles = await userManager.GetRolesAsync(user);
                var selectlistOfRoles = new SelectList(otherRoles, "Name", "Name");
                
                return View(selectlistOfRoles);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost] // HTTP POST 
        public async Task<IActionResult> AssignRoleToUser(string role, string userid)
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
        public async Task<IActionResult> CreateRole()
        {
            try
            {
                var allroles = rolebll.GetAllRoles();
                return View(allroles);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(string rolename)
        {
            try
            {
                await roleManager.RoleExistsAsync(rolename);
                await roleManager.CreateAsync(new IdentityRole(rolename));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("CreateRole");
        }

        public async Task<IActionResult> DeleteRole()
        {
            try
            {
                ViewBag.AllRoles = rolebll.GetAllRoles();
                var allRoles = rolebll.GetAllRoles();
                var roleList = new SelectList(allRoles, "Name", "Name");
                return View(roleList);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string role)
        {
            try
            {
                var roleToDelete = rolebll.Get(r => r.Name == role);
                rolebll.GetAllRoles().Remove(roleToDelete);
                rolebll.Save();//make save function in RoleBLL
                return RedirectToAction("DeleteRole");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> DeleteUserRole(string userid)
        {
            try
            {
                var user = userbll.GetUserbyId(userid);//need argument inside of bracket
                var userRoles = await userManager.GetRolesAsync(user);
                var allRoles = rolebll.GetAllRoles();


                ViewBag.User = userbll.GetUserbyId(userid);//should use data
                ViewBag.UserRoles = await userManager.GetRolesAsync(user);
                var selectlistOfRoles = new SelectList(userRoles);
                return View(selectlistOfRoles);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUserRole(string role, string userid)
        {
            try
            {
                var user = userbll.GetUserbyId(userid);
                var roleId = rolebll.Get(r => r.Name == role).Id;
                await userManager.RemoveFromRoleAsync(user, role);
                await userManager.UpdateAsync(user);
                //var userRole = _context.UserRoles.First(ur => ur.UserId == userid && ur.RoleId == roleId);
                //rolebll.GetAllRoles().Remove(userRole);


                
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> DeleteUser()
        {
            try
            {
                ViewBag.AllUsers = userbll.GetAllUsers();
                var Alluser = userbll.GetAllUsers();
                var userList = new SelectList(Alluser, "Email", "Email");
                return View(userList);
                var users = userbll.GetAllUsers();
                var users1 = userbll.GetAllUsers();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> DeleteUser(string username)
        {
            try
            {
                var user = userbll.Get(u => u.Email == username);//make get function in appuser bll



                userbll.GetAllUsers().Remove(user);
                userbll.Save();
                return RedirectToAction("DeleteUser");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }        
    }
}