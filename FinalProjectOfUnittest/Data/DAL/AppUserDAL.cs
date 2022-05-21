using FinalProjectOfUnittest.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectOfUnittest.Data.DAL
{
    public class AppUserDAL : IDAL<AppUser>
    {
        public ApplicationDbContext Context { get; set; }
        public AppUserDAL(ApplicationDbContext context)
        {
            Context = context;
        }

        //Create
        public void Add(AppUser appuser)
        {
            Context.Add(appuser);
        }
      
        //Read
        public AppUser GetbyId(string id)
        {
            return Context.AppUser.First(a => a.Id == id);
        }
        public AppUser Get(Func<AppUser, bool> firstFuction)
        {
            var allusers = Context.AppUser.Include(u => u.ProjectUsers);
            return Context.AppUser.FirstOrDefault(firstFuction);
            
        }
        public ICollection<AppUser> GetAll()
        {
            return Context.AppUser.ToList();//convert the type from database to list
        }
        public ICollection<AppUser> GetList(Func<AppUser, bool> func)
        {
            return Context.AppUser.Where(func).ToList();
        }

        //Update
        public void Update(AppUser a)
        {
            Context.AppUser.Update(a);
        }

        public void Delete(AppUser a)
        {
            Context.AppUser.Remove(a);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
