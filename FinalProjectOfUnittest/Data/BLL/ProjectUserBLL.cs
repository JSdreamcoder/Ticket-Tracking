using FinalProjectOfUnittest.Data.DAL;
using FinalProjectOfUnittest.Models;

namespace FinalProjectOfUnittest.Data.BLL
{
    public class ProjectUserBLL
    {
        ProjectUserDAL projectUserDAL;
        public ProjectUserBLL(ProjectUserDAL pd)
        {
            projectUserDAL = pd;
        }

        public void Add(ProjectUser u)
        {
            projectUserDAL.Add(u);
        }
        public ICollection<ProjectUser> GetAll()
        {
            return projectUserDAL.GetAll();
        }

        public ProjectUser Get(Func<ProjectUser, bool> func)
        {
            return projectUserDAL.Get(func);
        }
        
        public void Delete(ProjectUser pu)
        {
            projectUserDAL.Delete(pu);
        }
        public void Save()
        {
            projectUserDAL.Save();
        }
    }
}
