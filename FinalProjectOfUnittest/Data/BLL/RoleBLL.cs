using FinalProjectOfUnittest.Data.DAL;
using Microsoft.AspNetCore.Identity;

namespace FinalProjectOfUnittest.Data.BLL
{
    //declare dal first
    public class RoleBLL
    {
        public RoleDAL RoleDAL;

        public RoleBLL(RoleDAL rd)
        {
            RoleDAL = rd;
        }

        //To get all list check RoleDAL GETALL
        public ICollection<IdentityRole> GetAllRoles()
        {
            return RoleDAL.GetAll();
        }
        public IdentityRole Get(Func<IdentityRole, bool> firstFuction)
        {
            return RoleDAL.Get(firstFuction);
        }

        public List<IdentityRole> GetCounterPartRoles(IList<string> userroles)
        {
            if (userroles == null )
            {
                throw new ArgumentNullException("userroles never be null");
            }else if (userroles.Count == 0)
            {
                throw new ArgumentNullException("there should be at least 1 role");
            }
                var result = GetAllRoles().Where(r => !userroles.Contains(r.ToString())).ToList();
            return result;
        }
        //SAVE FUNCTION
        public void Save()
        {
            RoleDAL.Save();
        }
    }
}
