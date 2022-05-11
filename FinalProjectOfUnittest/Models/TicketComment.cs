namespace FinalProjectOfUnittest.Models
{
    public class TicketComment
    {
        public int Id { get; set; }

        public string Comment { get; set; } 

        public DateTime Created { get; set; }
       
        public string UserId { get; set; }
        public AppUser? User { get; set; }

        public int TicketId { get; set; }
        public Ticket? Ticket { get; set; }
    }
}
