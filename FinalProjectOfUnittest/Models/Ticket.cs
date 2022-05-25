using System.ComponentModel.DataAnnotations;
namespace FinalProjectOfUnittest.Models
{
    public class Ticket//for inherit
    {
        public DeadLineStrategy DeadlineStrategy { get; set; }//add 
        public int Id { get; set; } 
        public string Title { get; set; }   
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
        public virtual TicketTypes TicketType { get; set; }//add
        public TicketPriorities TicketPriority{ get; set; }
        public TicketStatus TicketStatus { get; set; }
        public string? OwnerUserId { get; set; } 
        public AppUser? OwnerUser { get; set; }
        public string? AssignedToUserId { get; set; }
        public AppUser? AssignedToUser { get; set; }
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
        public int ResponseDeadLine { get; set; } // for set assignmentuser deadline
        public int BreachDeadLine { get; set; } // for set Complete Deadline
        // type = bugreport (high priority)  == responseDL = 10 , brechDl =24(import)
        // type = payment  ( medium pri) ==> reDL = 20 , beachDl = 72
        // type = GeneralQuest (low prioryt) ==>responseDe = 72, breadl = 1000(no import)

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

    // Decorator Patterns
    public abstract class SpecialTickets : Ticket
    {
        public Ticket Ticket { get; set; }
        public override TicketTypes TicketType 
        { 
            get => base.TicketType; 
            set => base.TicketType = value;
        }
    }
    public class BugReports : SpecialTickets
    {
        public string ErrorCodes { get; set; }
        public string ErrorLogs { get; set; }
    }
    public class ServiceRequests : SpecialTickets
    {
        public override TicketTypes TicketType { get; set; }
    }
    // Factory patterns
    public class factory
    {
        public DeadLineStrategy DeadLineStrategy { get; set; }
        public Ticket CreateTicket(int id, string title, string description, TicketTypes type)
        {
            // 1. create Ticket
            var newticket = new Ticket { Id = id, Title = title, Description = description };
            // 2. choose strategy
            if (type == TicketTypes.GeneralQuestion)// low
            {
                DeadLineStrategy = new LowPriorityDeadLine();
            }
            else if (type == TicketTypes.Payment || type == TicketTypes.TechIssue)//medium
            {
                DeadLineStrategy = new MediumPriortyDeadLine();
            }
            else //high
            {

                DeadLineStrategy = new MediumPriortyDeadLine();
            }
            // 3. run setDeadline of the LowPriorityDealine strategy
            DeadLineStrategy.SetDeadLine(newticket);
            // 4 return
            return newticket;
        }
    }

    //  Strategy patterns
    public interface DeadLineStrategy
    {
        public void SetDeadLine(Ticket ticket);
    }
    public class LowPriorityDeadLine : DeadLineStrategy
    {
        public void SetDeadLine(Ticket ticket)
        {
            ticket.ResponseDeadLine = 100;
            ticket.BreachDeadLine = 1000;
        }
    }
    public class MediumPriortyDeadLine : DeadLineStrategy
    {
        public void SetDeadLine(Ticket ticket)
        {
            ticket.ResponseDeadLine = 50;
            ticket.BreachDeadLine = 100;
        }
    }
    public class HighPriorityDeadLine : DeadLineStrategy
    {
        public void SetDeadLine(Ticket ticket)
        {
            ticket.ResponseDeadLine = 10;
            ticket.BreachDeadLine = 24;
        }
    }
}
