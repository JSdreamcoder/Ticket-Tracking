using FinalProjectOfUnittest.Models;
using Microsoft.EntityFrameworkCore;
// ctor  -- Making construction
namespace FinalProjectOfUnittest.Data.DAL
{
    public class ProjectDAL : IDAL<Project>
    {
        public ApplicationDbContext Context { get; set; }
        
        public ProjectDAL(ApplicationDbContext context)
        {
            Context = context;
        }
        public ProjectDAL()
        {
                
        }
        //Create
        public void Add(Project project)
        {
            Context.Add(project);
        }

        //Read
        
        public virtual Project Get(int id)
        {
            var projects = Context.Project.Include(p => p.Tickets)
                                          .Include(p=>p.ProjectUsers).ThenInclude(pu=>pu.User);
            return projects.First(a => a.Id == id);
        }
        public Project Get(Func<Project, bool> firstFuction)
        {
            var projects = Context.Project.Include(p => p.Tickets)
                                          .Include(p => p.ProjectUsers);
            return projects.First(firstFuction);
            
        }
        public virtual ICollection<Project> GetAll()
        {
            var projects = Context.Project.Include(p => p.Tickets)
                                          .Include(p => p.ProjectUsers);
            return projects.ToList();
        }
        public ICollection<Project> GetList(Func<Project, bool> func)
        {
            var projects = Context.Project.Include(p => p.Tickets)
                                          .Include(p => p.ProjectUsers);
            return projects.Where(func).ToList();
        }

        //Update
        public void Update(Project a)
        {
            Context.Project.Update(a);
        }

        public void Delete(Project a)
        {
            Context.Project.Remove(a);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
