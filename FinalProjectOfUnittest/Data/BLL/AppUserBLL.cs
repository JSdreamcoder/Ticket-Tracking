using FinalProjectOfUnittest.Data.DAL;
using FinalProjectOfUnittest.Models;
using Microsoft.AspNetCore.Identity;

namespace FinalProjectOfUnittest.Data.BLL
{
    public class AppUserBLL
    {
        public AppUserDAL AppUserDAL;
        public UserManager<AppUser> userManager;
        public AppUserBLL(AppUserDAL adal)
        {
            AppUserDAL = adal;
        }
        public AppUserBLL(AppUserDAL adal,UserManager<AppUser> um)
        {
            AppUserDAL = adal;
            userManager = um;
        }
        
        public AppUser GetUserbyId(string Id)
        {
            
            if (Id == null)
            {
                throw new Exception("Id never be null");
            }

            return AppUserDAL.GetbyId(Id);
        }

        public ICollection<AppUser> GetAllUsers()
        {
            return AppUserDAL.GetAll();//check GETALL in AppUserDal line29
        }

        public AppUser Get(Func<AppUser, bool> firstFunction)
        {
            
            return AppUserDAL.Get(firstFunction);//get data from DAL to BLL
        }

        public void Update(AppUser user)
        {
            AppUserDAL.Update(user);
        }

        public void Save()
        {
            AppUserDAL.Save();
        }
    }
}
