using Microsoft.AspNetCore.Identity;

namespace FinalProjectOfUnittest.Data.DAL
{
    public class RoleDAL : IDAL<IdentityRole>
    {
        public ApplicationDbContext Context { get; set; }
        public RoleDAL(ApplicationDbContext context)
        {
            Context = context;
        }

        //Create
        public void Add(IdentityRole appuser)
        {
            Context.Add(appuser);
        }

        //Read
        public IdentityRole Get(string id)
        {
            return Context.Roles.First(a => a.Id == id);
        }

        //useding in Role BLL
        public IdentityRole Get(Func<IdentityRole, bool> firstFuction)
        {
            return Context.Roles.First(firstFuction);

        }
        public ICollection<IdentityRole> GetAll()
        {
            return Context.Roles.ToList();
        }
        public ICollection<IdentityRole> GetList(Func<IdentityRole, bool> func)
        {
            return Context.Roles.Where(func).ToList();
        }

        //Update
        public void Update(IdentityRole a)
        {
            Context.Update(a);
        }

        public void Delete(IdentityRole a)
        {
            Context.Remove(a);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
