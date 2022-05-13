using FinalProjectOfUnittest.Data.DAL;
using FinalProjectOfUnittest.Models;

namespace FinalProjectOfUnittest.Data.BLL
{
    public class AppUserBLL
    {
        public AppUserDAL AppUserDAL;
        public AppUserBLL(AppUserDAL adal)
        {
            AppUserDAL = adal;
        }

        public AppUser GetUserbyId(string Id)
        {
            return AppUserDAL.GetbyId(Id);
        }

    }
}
