namespace FinalProjectOfUnittest.Models
{
    public class ProjectUser
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Prject { get; set; }
        public string UserId { get; set; }

    }
}
