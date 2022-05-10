namespace FinalProjectOfUnittest.Models
{
    public class TicketNotification
    {
        public int Id { get; set; }
        
        public string UserId { get; set; }

        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
}
