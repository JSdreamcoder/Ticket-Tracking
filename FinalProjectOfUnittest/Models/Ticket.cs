using FinalProjectOfUnittest.Controllers;
using System.ComponentModel.DataAnnotations;

namespace FinalProjectOfUnittest.Models
{
    public class Ticket
    {
        public virtual int Id { get; set; } 
        public virtual string Title { get; set; }   
        public virtual string Description { get; set; }

        public virtual DateTime Created { get; set; }
        public virtual DateTime? Updated { get; set; }
        public virtual int ProjectId { get; set; }
        public Project? Project { get; set; }
        public virtual TicketTypes TicketType { get; set; }
        
        public virtual TicketPriorities TicketPriority { get; set; }
        
        public virtual TicketStatus TicketStatus { get; set; }
        
        public virtual string? OwnerUserId { get; set; } 
        public AppUser? OwnerUser { get; set; }
        public virtual string? AssignedToUserId { get; set; }
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

        public virtual int ResponseDeadLine {get; set;} // for set assignmentuser deadline
        public virtual int BreachDeadLine { get; set;} // for set Complete Deadline

  


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



    /*
     * 1 Add Dead lines to Ticket - All tickets should have dead lines
     * 
     * -- Decorator patter--
     * 2 Making new Ticket classes (Bugreport,SurviceRequest ... ) 
     *    - Set all property of ticket virtual - All properties should be overrded at decorator
     *    - Making astract Docorator has all override properties from tickt(need a little bit special setting of get,set to send the property data to chilren) 
     *    - Making several children of decorator
     *    
     *    
     *    
     * --Strategy Pattern--
     * 3 Making strategy of DeadLine by ticket type and error code
     * 
     * 
     * 
     * -- Factory Pattern --
     * 4 Making Ticket Creating system class
     *    - Has 3 overload CreateTicket Function (normal,bugreport,serviceRequest)
     *    - each Create Function implement set priority fuction and set deadline function.
    */



    // Decorator Pattern
    public abstract class AddOnToTicket : Ticket
    {
        public Ticket Ticket { get; set; }
        public override int Id { get=>Ticket.Id; set=>Ticket.Id = value; }
        public override string Title { get=>Ticket.Title; set=>Ticket.Title = value; }
        public override string Description { get=>Ticket.Description; set=>Ticket.Description = value; }
        public override DateTime Created { get=>Ticket.Created; set=>Ticket.Created = value; }  
        public override DateTime? Updated { get=>Ticket.Updated; set=>Ticket.Updated = value; }
        public override int ProjectId { get=>Ticket.ProjectId; set=>Ticket.ProjectId = value; }
        public override TicketTypes TicketType { get=>Ticket.TicketType; set=>Ticket.TicketType = value; }
        public override TicketPriorities TicketPriority { get=>Ticket.TicketPriority; set=>Ticket.TicketPriority = value; }
        public override TicketStatus TicketStatus { get=>Ticket.TicketStatus; set=>Ticket.TicketStatus = value; }
        public override string? OwnerUserId { get=>Ticket.OwnerUserId; set=>Ticket.OwnerUserId = value; }
        public override string? AssignedToUserId { get=>Ticket.AssignedToUserId; set=>Ticket.AssignedToUserId = value; }
        public override int ResponseDeadLine { get => Ticket.ResponseDeadLine; set => Ticket.ResponseDeadLine = value; } // for set assignmentuser deadline
        public override int BreachDeadLine { get => Ticket.BreachDeadLine; set=>Ticket.BreachDeadLine=value; } // for set Complete Deadline
    }

    public class Bugreports : AddOnToTicket
    {

        public Bugreports(Ticket t)
        {
            Ticket = t; 
        }

        public string ErrorCodes { get; set; } 
        public string ErrorLogs { get; set; } 
    }
    public class ServiceRequest : AddOnToTicket 
    {
        public ServiceRequest(Ticket t)
        {
            Ticket = t;
        }
        public ServiceRequestType ServiceRequestType;
    }
    public enum ServiceRequestType
    {
        CustomerCall,
        DefectiveProduct,
        FiedlService
    }

    public class AccountRequest : AddOnToTicket
    { 
        public AccountRequest(Ticket t)
        {
            Ticket = t;
        }
    }
    public class TechRequest : AddOnToTicket 
    {
        public TechRequest(Ticket t)
        {
            Ticket=t;
        }
    }
    public class GeneralQuestion : AddOnToTicket 
    {
        public GeneralQuestion(Ticket t)
        {
            Ticket =t;
        }
    }
    public class PaymentRequest : AddOnToTicket 
    {
        public PaymentRequest(Ticket t)
        {
            Ticket = t;
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

    public class Emergency : DeadLineStrategy
    {
        public void SetDeadLine(Ticket ticket)
        {
            ticket.ResponseDeadLine = 6;
            ticket.BreachDeadLine = 24;
        }
    }

    // factory pattern
    public class TicketFactory
    {
        DeadLineStrategy DeadLineStrategy;

        TicketFactory(DeadLineStrategy ds)
        {
            DeadLineStrategy = ds;
        }
        // create ticket has only common properties
        public Ticket CreateTicket(int id, string title, string description,DateTime created,int projectid, TicketTypes type,string owneruserid)
        {
            Ticket newTicket = new Ticket()
            {
                Id = id,
                Title = title,
                Description = description,
                Created = created,
                ProjectId = projectid,
                TicketType = type,
                OwnerUserId = owneruserid,
                TicketStatus = TicketStatus.Submitted
            };
            newTicket = SetDeadLines(newTicket);
            newTicket = SetTicketPriority(newTicket);

            if (type == TicketTypes.AccountIssue)
            {
                return new AccountRequest(newTicket);
               
            }
            else if (type == TicketTypes.Payment)
            {
                return new PaymentRequest(newTicket);
            }
            else if (type == TicketTypes.TechIssue)
            {
                return new TechRequest(newTicket);
            }
            else if (type == TicketTypes.GeneralQuestion)
            {
                return new GeneralQuestion(newTicket);
            }
            else
            {
                throw new Exception("Wrong Type");
            }
           
        }
        //Create BugReport
        public Ticket CreateTicket(int id, string title, string description, DateTime created, int projectid, TicketTypes type, string owneruserid, string errorCode, string errorLog)
        {
            if (type == TicketTypes.BugReport)
            {
                var newTicket = new Ticket()
                {
                    Id = id,
                    Title = title,
                    Description = description,
                    Created = created,
                    ProjectId = projectid,
                    TicketType = type,
                    OwnerUserId = owneruserid,
                    TicketStatus = TicketStatus.Submitted
                };
                newTicket = SetTicketPriority(newTicket);

                Bugreports bugreport = new Bugreports(newTicket) { ErrorCodes = errorCode, ErrorLogs = errorLog };
                bugreport = SetBugReportDeadLine(bugreport);
                
               
                return bugreport ;
            }
            else
            {
                throw new Exception("Wrong Type");
            }
        }
        //Create ServiceRequest
        public Ticket CreateTicket(int id, string title, string description, DateTime created, int projectid, TicketTypes type, string owneruserid, ServiceRequestType servicerequesttype)
        {
            if (type == TicketTypes.ServiceRequest)
            {
                var newTicket = new Ticket()
                {
                    Id = id,
                    Title = title,
                    Description = description,
                    Created = created,
                    ProjectId = projectid,
                    TicketType = type,
                    OwnerUserId = owneruserid,
                    TicketStatus = TicketStatus.Submitted
                };
                newTicket = SetTicketPriority(newTicket);
                newTicket = SetDeadLines(newTicket);
                
                return new ServiceRequest(newTicket) { ServiceRequestType = servicerequesttype};
            }
            else
            {
                throw new Exception("Wrong Type");
            }
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

                case TicketTypes.ServiceRequest:
                case TicketTypes.Payment:
                case TicketTypes.TechIssue:
                    ticket.TicketPriority = TicketPriorities.Medium;
                    break;

                
                case TicketTypes.GeneralQuestion:
                    ticket.TicketPriority = TicketPriorities.Low;
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

        public Bugreports SetBugReportDeadLine(Bugreports bugreport)
        {
            if (bugreport.TicketPriority == TicketPriorities.High && bugreport.ErrorCodes == "ErrorCode : 101")
            {
                DeadLineStrategy = new Emergency();
            }else
            {
                DeadLineStrategy = new ShortDeadLine();
            }

            // run setDeadline
            DeadLineStrategy.SetDeadLine(bugreport);

            // return
            return bugreport;
        }

    };

    

   


 


}
