using FinalProjectOfUnittest.Models;

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
        public AppUser Get(String id)
        {
            return Context.AppUser.First(a => a.Id == id);
        }
        public AppUser Get(Func<AppUser, bool> firstFuction)
        {
            return Context.AppUser.First(firstFuction);
            
        }
        public ICollection<AppUser> GetAll()
        {
            return Context.AppUser.ToList();
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
