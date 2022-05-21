namespace FinalProjectOfUnittest.Models
{
    public class TicketAttachment
    {
        public int Id { get; set; }
        

        public string FilePath { get; set; }

        public string? Description { get; set; } // I dont know how to use this
        public DateTime Created { get; set; }

        public string UserId { get; set; }
        public AppUser? User { get; set; }
        public string? FileUrl { get; set; }// I dont know how to use this

        public int TicketId { get; set; }
        public Ticket? ticket { get; set; }

    }
}
