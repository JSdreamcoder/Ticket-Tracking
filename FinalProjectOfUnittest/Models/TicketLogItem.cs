namespace FinalProjectOfUnittest.Models
{
    public class TicketLogItem
    {
        public int Id { get; set; }

        public int TicketHistoryId { get; set; }
        public TicketHistory TicketHistory { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int ProjectId { get; set; }
        public TicketTypes TicketType { get; set; }

        public TicketPriorities TicketPriority { get; set; }

        public TicketStatus TicketStatus { get; set; }

        public string? OwnerUserId { get; set; }

        public string? AssignedToUserId { get; set; }

    }
}
