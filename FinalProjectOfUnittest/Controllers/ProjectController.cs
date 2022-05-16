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
    public class ProjectController : Controller
    {
        private readonly ProjectBLL projectbll;
        private readonly AppUserBLL userbll;
        public ProjectController(ApplicationDbContext context)
        {
            projectbll = new ProjectBLL( new ProjectDAL(context));
            userbll = new AppUserBLL(new AppUserDAL(context));
        }

        // GET: Projects
        public IActionResult Index()
        {
            return View(projectbll.GetAll());
        }

       
        public IActionResult Details(int id)
        {
            if (id == null || projectbll.GetAll() == null)
            {
                return NotFound();
            }

            var project = projectbll.GetById(id);   
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name")] Project project)
        {
            if (ModelState.IsValid)
            {
                projectbll.Create(project);
                projectbll.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || projectbll.GetAll() == null)
            {
                return NotFound();
            }

            var project = projectbll.GetById(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    projectbll.Update(project);
                    projectbll.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
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
            return View(project);
        }

        public async Task<IActionResult> AssignUser(int projectId)
        {
            try
            {
                var project = projectbll.GetById(projectId);
                //var alluser
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return View();
        }

        private bool ProjectExists(int id)
        {
            return (projectbll.GetAll()?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
