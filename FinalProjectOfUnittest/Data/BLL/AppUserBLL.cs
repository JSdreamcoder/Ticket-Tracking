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

        public void Save()
        {
            AppUserDAL.Save();
        }
    }
}
