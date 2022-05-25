using System.ComponentModel.DataAnnotations;
namespace FinalProjectOfUnittest.Models
{
    public class Ticket//for inherit
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
        public TicketTypes TicketType { get; set; }
        public TicketPriorities TicketPriority { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public string? OwnerUserId { get; set; }
        public AppUser? OwnerUser { get; set; }
        public string? AssignedToUserId { get; set; }
        public AppUser? AssignedToUser { get; set; }
        public ICollection<TicketAttachment> TicketAttachments { get; set; }
        public ICollection<TicketComment> TicketComments { get; set; }
        public ICollection<TicketHistory> TicketHistories { get; set; }
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
        TicketStatus,
        Submitted,
        Assigned,
        Completed
    }

    public enum TicketPriorities
    {
        TicketPriority,
        High,
        Medium,
        Low
    }

    public enum TicketTypes
    {
        TicketType,
        GeneralQuestion,  // Low
        BugReport,        // High
        Payment,          // Medium
        TechIssue,        // Medium
        AccountIssue      // High
    }


    public enum SortbyList
    {
        [Display(Name = "Select Sort Way")]
        Sortby,
        Submitter,
        [Display(Name = "Assigned Staff")]
        AssignedStaff,
        [Display(Name = "Newest Created Date")]
        NewestCreate,
        [Display(Name = "Newest Updated Date")]
        NewestUpdate,
        [Display(Name = "Ticket Type")]
        TicketType,
        [Display(Name = "Ticket Priority")]
        TicketPriority,
        [Display(Name = "Ticket Status")]
        TicketStatus

    }
}

