namespace FinalProjectOfUnittest.Models
{
    public class ProjectUser
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
        public string UserId { get; set; }
        public AppUser? User { get; set; }

    }
}
