using FinalProjectOfUnittest.Data.DAL;
using Microsoft.AspNetCore.Identity;

namespace FinalProjectOfUnittest.Data.BLL
{

    public class RoleBLL
    {
        public RoleDAL RoleDAL;
        public RoleBLL(RoleDAL rd)
        {
            RoleDAL = rd;
        }

        public ICollection<IdentityRole> GetAllRoles()
        {
            return RoleDAL.GetAll();
        }
    }
}
