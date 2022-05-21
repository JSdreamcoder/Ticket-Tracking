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
        public RoleDAL()
        {
           
        }

        //Create
        public virtual void Add(IdentityRole appuser)
        {
            Context.Add(appuser);
        }

        //Read
        public virtual IdentityRole GetbyId(string id)
        {
            return Context.Roles.First(a => a.Id == id);
        }

        //useding in Role BLL
        public virtual IdentityRole Get(Func<IdentityRole, bool> firstFuction)
        {
            return Context.Roles.First(firstFuction);

        }
        public virtual ICollection<IdentityRole> GetAll()
        {
            return Context.Roles.ToList();
        }
        public virtual ICollection<IdentityRole> GetList(Func<IdentityRole, bool> func)
        {
            return Context.Roles.Where(func).ToList();
        }

        //Update
        public virtual void Update(IdentityRole a)
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
