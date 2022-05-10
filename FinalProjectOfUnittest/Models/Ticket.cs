namespace FinalProjectOfUnittest.Models
{
    public class Ticket
    {
        public int Id { get; set; } 
        public string Title { get; set; }   
        public string Description { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int ProjectId { get; set; }

        public int TicketTypeId { get; set; }
        public int TicketPriorityId { get; set; }

        public int TicketStatusId { get; set; }
        public string OwnerUserId { get; set; }

        public string AssignedToUserId { get; set; }

        public ICollection<TicketAttachment> TicketAttachments { get; set; }
        public ICollection<TicketComment> TicketComments { get; set; }
        public ICollection<TicketHistory> TicketHistories { get; set;}
        public ICollection<TicketNotification> TicketNotifications { get; set; }
        public Ticket()
        {
            TicketAttachments = new HashSet<TicketAttachment>();
            TicketComments = new HashSet<TicketComment>();
            TicketHistories = new HashSet<TicketHistory>();
            TicketNotifications = new HashSet<TicketNotification>();

        }

    }

    public enum TicketStatus
    {

    }

    public enum TicketPriorities
    {

    }

    public enum TicketTypes
    { 


    }



}
