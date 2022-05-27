using FinalProjectOfUnittest.Models;

namespace FinalProjectOfUnittest.Data.DAL
{
    public class TicketLogItemDAL : IDAL<TicketLogItem>
    {
        public ApplicationDbContext Context { get; set; }
        public TicketLogItemDAL(ApplicationDbContext context)
        {
            Context = context;
        }

        //Create
        public void Add(TicketLogItem ticketLogitem)
        {
            Context.Add(ticketLogitem);
        }

        //Read
        public TicketLogItem Get(int id)
        {
            return Context.TicketLogItem.First(a => a.Id == id);
        }
        public TicketLogItem Get(Func<TicketLogItem, bool> firstFuction)
        {
            return Context.TicketLogItem.First(firstFuction);

        }
        public ICollection<TicketLogItem> GetAll()
        {
            return Context.TicketLogItem.ToList();
        }
        public ICollection<TicketLogItem> GetList(Func<TicketLogItem, bool> func)
        {
            return Context.TicketLogItem.Where(func).ToList();
        }

        //Update
        public void Update(TicketLogItem ticketLogitem)
        {
            Context.TicketLogItem.Update(ticketLogitem);
        }

        public void Delete(TicketLogItem ticketLogitem)
        {
            Context.TicketLogItem.Remove(ticketLogitem);
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public TicketLogItemDAL()
        {

        }
    }
}
