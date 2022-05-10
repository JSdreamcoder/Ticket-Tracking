namespace FinalProjectOfUnittest.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }

        ICollection<ProjectUser> ProjectUsers { get; set; }
        Project()
        {
            ProjectUsers = new HashSet<ProjectUser>();
        }
    }
}
