using FinalProjectOfUnittest.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectOfUnittest.Data.DAL
{
    public class TicketDAL : IDAL<Ticket>
    {
        public ApplicationDbContext Context { get; set; }
        public TicketDAL(ApplicationDbContext context)
        {
            Context = context;
        }

        //Create
        public void Add(Ticket appuser)
        {
            Context.Add(appuser);
        }

        //Read
        public Ticket Get(int id)
        {
            var db = Context.Ticket.Include(t => t.TicketHistories)
                                  .Include(t => t.TicketComments).ThenInclude(c=>c.User)
                                  .Include(t => t.TicketNotifications)
                                  .Include(t => t.TicketAttachments)
                                  .Include(t => t.Project)
                                  .Include(t => t.OwnerUser)
                                  .Include(t => t.AssignedToUser);
            return db.First(a => a.Id == id);
        }
        public Ticket Get(Func<Ticket, bool> firstFuction)
        {
            return Context.Ticket.First(firstFuction);

        }
        public ICollection<Ticket> GetAll()
        {
           var db = Context.Ticket.Include(t => t.TicketHistories)
                                  .Include(t => t.TicketComments).ThenInclude(c => c.User)
                                  .Include(t => t.TicketNotifications)
                                  .Include(t => t.TicketAttachments)
                                  .Include(t => t.Project)
                                  .Include(t => t.OwnerUser)
                                  .Include(t => t.AssignedToUser);
            return db.ToList();
        }
        public ICollection<Ticket> GetList(Func<Ticket, bool> func)
        {
            return Context.Ticket.Where(func).ToList();
        }

        //Update
        public void Update(Ticket a)
        {
            Context.Ticket.Update(a);
        }

        public void Delete(Ticket a)
        {
            Context.Ticket.Remove(a);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
