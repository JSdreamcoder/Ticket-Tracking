namespace FinalProjectOfUnittest.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ProjectUser> ProjectUsers { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public Project()
        {
            ProjectUsers = new HashSet<ProjectUser>();
            Tickets = new HashSet<Ticket>();    
        }
    }
}
