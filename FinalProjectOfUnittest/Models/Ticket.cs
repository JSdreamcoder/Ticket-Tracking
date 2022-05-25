using FinalProjectOfUnittest.Controllers;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectOfUnittest.Models
{
    public class Ticket
    {
        public DeadLineStrategy DeadlineStrategy { get; set; }
        public int Id { get; set; } 
        public string Title { get; set; }   
        public string Description { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
        public virtual TicketTypes TicketType { get; set; }
        
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

        public int ResponseDeadLine {get; set;} // for set assignmentuser deadline
        public int BreachDeadLine { get; set;} // for set Complete Deadline

        public string ErrorCodes { get; set; } // for bugreport
        public string ErrorLogs { get; set; } // for bugreport


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
        AccountIssue,      // High
        ServiceRequest
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

    public class Bugreports : Ticket { }
    public class ServiceRequest : Ticket { }
    public class AccountRequest : Ticket { }
    public class TechRequest : Ticket { }
    public class GeneralQuestion : Ticket { }
    public class PaymentRequest : Ticket { }

    public class TicketFactory
    {
        public Ticket CreateTicket(int id, string title, string description, TicketTypes type)
        {
            Ticket newTicket;

            if (type == TicketTypes.AccountIssue)
            {
                newTicket = new AccountRequest { Id = id, Title = title, Description = description, TicketType = type };
            }
            else if (type == TicketTypes.Payment)
            {
                newTicket = new PaymentRequest { Id = id, Title = title, Description = description, TicketType = type };
            }
            else if (type == TicketTypes.TechIssue)
            {
                newTicket = new TechRequest { Id = id, Title = title, Description = description, TicketType = type };
            }
            else if (type == TicketTypes.ServiceRequest)
            {
                newTicket = new ServiceRequest { Id = id, Title = title, Description = description, TicketType = type };
            }
            else if (type == TicketTypes.GeneralQuestion)
            {
                newTicket = new GeneralQuestion { Id = id, Title = title, Description = description, TicketType = type };
            }
            else
            {
                throw new Exception("Wrong Type");
            }
            return newTicket;
        }

        public Ticket CreateTicket(int id, string title, string description, TicketTypes type, string errorCode, string errorLog)
        {
            if (type == TicketTypes.BugReport)
            {
                return new Bugreports { Id = id, Title = title, Description = description, TicketType = type, ErrorCodes = errorCode, ErrorLogs = errorLog };
            }
            else
            {
                throw new Exception("Wrong Type");
            }
        }
    }

    // Factory patterns
    public class TicketSystem
    {
        public DeadLineStrategy DeadLineStrategy { get; set; }
        public TicketFactory TicketFactory { get; set; }
        public TicketSystem()
        {
            TicketFactory = new TicketFactory();
        }
        //create Ticket( 1.create object 2.setpriority by type 2.set deadline by priority 
        // create tickets 
        public Ticket CreateTicket(int id, string title, string description, TicketTypes type)
        {
            var newTicket = TicketFactory.CreateTicket(id, title, description, type);
            newTicket = SetTicketPriority(newTicket);
            newTicket = SetDeadLines(newTicket);

            return newTicket;
        }
        // create tickets with errorCode and error logs
        public Ticket CreateTicket(int id, string title, string description, TicketTypes type, string errorCode, string errorLog)
        {
            Ticket newTicket = TicketFactory.CreateTicket(id, title, description, type, errorCode, errorLog);
            newTicket = SetTicketPriority(newTicket);
            newTicket = SetDeadLines(newTicket);

            return newTicket;
        }

        public Ticket SetTicketPriority(Ticket ticket)
        {
            //BugReport,        // High
            //AccountIssue      // High
            //Payment,          // Medium
            //TechIssue,        // Medium
            //ServiceRequest    // Medium
            //GeneralQuestion,  // Low 
            switch (ticket.TicketType)
            {
                case TicketTypes.BugReport:
                case TicketTypes.AccountIssue:
                    ticket.TicketPriority = TicketPriorities.High;
                    break;

                case TicketTypes.Payment:
                case TicketTypes.TechIssue:
                    ticket.TicketPriority = TicketPriorities.Medium;
                    break;

                case TicketTypes.ServiceRequest:
                case TicketTypes.GeneralQuestion:
                    ticket.TicketPriority = TicketPriorities.Medium;
                    break;
            }

            return ticket;
        }
        public Ticket SetDeadLines(Ticket newticket)
        {

            // strategy selected by priority
            if (newticket.TicketPriority == TicketPriorities.Low)// low
            {
                DeadLineStrategy = new LongDeadLine();

            }
            else if (newticket.TicketPriority == TicketPriorities.Medium)//medium
            {
                DeadLineStrategy = new MediumDeadLine();
            }
            else //high
            {
                DeadLineStrategy = new ShortDeadLine();
            }

            // run setDeadline
            DeadLineStrategy.SetDeadLine(newticket);

            // return
            return newticket;
        }


    }


    //  Strategy patterns
    public interface DeadLineStrategy
    {
        public void SetDeadLine(Ticket ticket);
    }

    public class LongDeadLine : DeadLineStrategy
    {
        public void SetDeadLine(Ticket ticket)
        {
            ticket.ResponseDeadLine = 72;
            ticket.BreachDeadLine = 240;
        }
    }
    public class MediumDeadLine : DeadLineStrategy
    {
        public void SetDeadLine(Ticket ticket)
        {
            ticket.ResponseDeadLine = 48;
            ticket.BreachDeadLine = 120;
        }

    }
    public class ShortDeadLine : DeadLineStrategy
    {
        public void SetDeadLine(Ticket ticket)
        {
            ticket.ResponseDeadLine = 24;
            ticket.BreachDeadLine = 72;
        }

    }


}
