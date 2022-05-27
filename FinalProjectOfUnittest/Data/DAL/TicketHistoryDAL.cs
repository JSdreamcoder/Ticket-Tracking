using FinalProjectOfUnittest.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectOfUnittest.Data.DAL
{
    public class TicketHistoryDAL : IDAL<TicketHistory>
    {
        public ApplicationDbContext Context { get; set; }
        public TicketHistoryDAL(ApplicationDbContext context)
        {
            Context = context;
        }

        //Create
        public void Add(TicketHistory appuser)
        {
            Context.Add(appuser);
        }

        //Read
        public TicketHistory Get(int id)
        {
            return Context.TicketHistory.First(a => a.Id == id);
        }
        public TicketHistory Get(Func<TicketHistory, bool> firstFuction)
        {
            return Context.TicketHistory.First(firstFuction);

        }
        public ICollection<TicketHistory> GetAll()
        {
            var db = Context.TicketHistory.Include(t => t.TicketLogItem)
                                          .Include(t=> t.Ticket);
            return db.ToList();
        }
        public ICollection<TicketHistory> GetList(Func<TicketHistory, bool> func)
        {
            var db = Context.TicketHistory.Include(t => t.TicketLogItem)
                                         .Include(t => t.Ticket);
            return db.Where(func).ToList();
        }

        //Update
        public void Update(TicketHistory a)
        {
            Context.TicketHistory.Update(a);
        }

        public void Delete(TicketHistory a)
        {
            Context.TicketHistory.Remove(a);
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public TicketHistoryDAL()
        {

        }
    }
}
