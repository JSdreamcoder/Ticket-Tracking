using FinalProjectOfUnittest.Models;

namespace FinalProjectOfUnittest.Data.DAL
{
    public class ProjectUserDAL : IDAL<ProjectUser>
    {
        public ApplicationDbContext Context { get; set; }
        public ProjectUserDAL(ApplicationDbContext context)
        {
            Context = context;
        }

        public ProjectUserDAL() { }




        //Create
        public void Add(ProjectUser appuser)
        {
            Context.ProjectUser.Add(appuser);
        }

        //Read
        
        public ProjectUser Get(int id)
        {
            return Context.ProjectUser.First(a => a.Id == id);
        }
        public ProjectUser Get(Func<ProjectUser, bool> firstFuction)
        {
            return Context.ProjectUser.First(firstFuction);

        }
        public ICollection<ProjectUser> GetAll()
        {
            return Context.ProjectUser.ToList();
        }
        public ICollection<ProjectUser> GetList(Func<ProjectUser, bool> func)
        {
            return Context.ProjectUser.Where(func).ToList();
        }

        //Update
        public void Update(ProjectUser a)
        {
            Context.ProjectUser.Update(a);
        }

        public void Delete(ProjectUser a)
        {
            Context.ProjectUser.Remove(a);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
