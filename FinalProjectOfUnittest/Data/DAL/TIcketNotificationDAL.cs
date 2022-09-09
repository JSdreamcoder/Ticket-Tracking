using FinalProjectOfUnittest.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectOfUnittest.Data.DAL
{
    public class TicketNotificationDAL : IDAL<TicketNotification>
    {
        public ApplicationDbContext Context { get; set; }
        public TicketNotificationDAL(ApplicationDbContext context)
        {
            Context = context;
        }

        //Create
        public void Add(TicketNotification appuser)
        {
            Context.Add(appuser);
        }

        //Read
        public TicketNotification Get(int id)
        {
            var ticketNotification = Context.TicketNotification
              .Include(t => t.Ticket)
              .Include(t => t.User)
              .First(m => m.Id == id);
            return ticketNotification;
        }
        public TicketNotification Get(Func<TicketNotification, bool> firstFuction)
        {
            var ticketNotification = Context.TicketNotification
              .Include(t => t.Ticket)
              .Include(t => t.User)
              .First(firstFuction);
            return ticketNotification;

        }
        public ICollection<TicketNotification> GetAll()
        {
            return Context.TicketNotification.ToList();
        }
        public ICollection<TicketNotification> GetList(Func<TicketNotification, bool> func)
        {
            var db = Context.TicketNotification.Include(t => t.Ticket);
            return db.Where(func).ToList();
        }

        //Update
        public void Update(TicketNotification a)
        {
            Context.TicketNotification.Update(a);
        }

        public void Delete(TicketNotification a)
        {
            Context.TicketNotification.Remove(a);
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public TicketNotificationDAL()
        {

        }
    }
}
