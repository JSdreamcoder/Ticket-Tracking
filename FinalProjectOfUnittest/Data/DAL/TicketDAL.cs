using FinalProjectOfUnittest.Models;

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
            return Context.Ticket.First(a => a.Id == id);
        }
        public Ticket Get(Func<Ticket, bool> firstFuction)
        {
            return Context.Ticket.First(firstFuction);

        }
        public ICollection<Ticket> GetAll()
        {
            return Context.Ticket.ToList();
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
