using FinalProjectOfUnittest.Models;

namespace FinalProjectOfUnittest.Data.DAL
{
    public class ProjectDAL : IDAL<Project>
    {
        public ApplicationDbContext Context { get; set; }
        public ProjectDAL(ApplicationDbContext context)
        {
            Context = context;
        }

        //Create
        public void Add(Project project)
        {
            Context.Add(project);
        }

        //Read
        public Project Get(int id)
        {
            return Context.Project.First(a => a.Id == id);
        }
        public Project Get(Func<Project, bool> firstFuction)
        {
            return Context.Project.First(firstFuction);
            
        }
        public ICollection<Project> GetAll()
        {
            return Context.Project.ToList();
        }
        public ICollection<Project> GetList(Func<Project, bool> func)
        {
            return Context.Project.Where(func).ToList();
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
