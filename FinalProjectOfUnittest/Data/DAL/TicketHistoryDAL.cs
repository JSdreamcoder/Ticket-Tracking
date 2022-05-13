using FinalProjectOfUnittest.Models;

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
            return Context.TicketHistory.ToList();
        }
        public ICollection<TicketHistory> GetList(Func<TicketHistory, bool> func)
        {
            return Context.TicketHistory.Where(func).ToList();
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
    }
}
