namespace FinalProjectOfUnittest.Models
{
    public class TicketHistory
    {
        public int Id { get; set; }
        
        public string Property { get; set; }

        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime Changed { get; set; }

        public string UserId { get; set; }
        public AppUser? User { get; set; }

        public int TicketId { get; set; }
        public Ticket? Ticket { get; set; }
    }
}
