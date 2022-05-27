using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProjectOfUnittest.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<TicketAttachment> TicketAttachments { get; set; }
        public ICollection<TicketComment> TicketComments { get; set; }
        public ICollection<TicketHistory> TicketHistories { get; set; }
        public ICollection<TicketNotification> TicketNotifications { get; set; }

        [InverseProperty("AssignedToUser")]//inverse
        public ICollection<Ticket> AssignedTicket { get; set; }
        [InverseProperty("OwnerUser")]
        public ICollection<Ticket> OwnerTicket { get; set; }
       
        public ICollection<ProjectUser> ProjectUsers { get; set; }
        public AppUser()
        {
            TicketAttachments = new HashSet<TicketAttachment>();
            TicketComments = new HashSet<TicketComment>();
            TicketHistories = new HashSet<TicketHistory>();
            TicketNotifications = new HashSet<TicketNotification>();
            OwnerTicket = new HashSet<Ticket>();
            AssignedTicket = new HashSet<Ticket>();
            ProjectUsers = new HashSet<ProjectUser>();
        }
    }
}
