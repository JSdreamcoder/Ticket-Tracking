using FinalProjectOfUnittest.Data.DAL;
using FinalProjectOfUnittest.Models;

namespace FinalProjectOfUnittest.Data.BLL
{
   
    public class ProjectBLL
    {
        ProjectDAL projectDAL;
        public ProjectBLL(ProjectDAL pd)
        {
            projectDAL = pd;
        }

        public void Create(Project newProject)
        {
            
            projectDAL.Add(newProject);
        }

        public Project GetById(int id)
        {
            return projectDAL.Get(id);
        }

        public ICollection<Project> GetAll()
        {
            return projectDAL.GetAll();
        }

        public void Update(Project project)
        {
            projectDAL.Update(project);
        }
        public void Save()
        {
            projectDAL.Save();
        }

        
    }
}
