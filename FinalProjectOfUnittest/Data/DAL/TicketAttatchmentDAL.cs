using FinalProjectOfUnittest.Models;

namespace FinalProjectOfUnittest.Data.DAL
{
    public class TicketAttachmentDAL : IDAL<TicketAttachment>
    {
        public ApplicationDbContext Context { get; set; }
        public TicketAttachmentDAL(ApplicationDbContext context)
        {
            Context = context;
        }

        //Create
        public void Add(TicketAttachment appuser)
        {
            Context.Add(appuser);
        }

        //Read
        public TicketAttachment Get(int id)
        {
            return Context.TicketAttachment.First(a => a.Id == id);
        }
        public TicketAttachment Get(Func<TicketAttachment, bool> firstFuction)
        {
            return Context.TicketAttachment.First(firstFuction);

        }
        public ICollection<TicketAttachment> GetAll()
        {
            return Context.TicketAttachment.ToList();
        }
        public ICollection<TicketAttachment> GetList(Func<TicketAttachment, bool> func)
        {
            return Context.TicketAttachment.Where(func).ToList();
        }

        //Update
        public void Update(TicketAttachment a)
        {
            Context.TicketAttachment.Update(a);
        }

        public void Delete(TicketAttachment a)
        {
            Context.TicketAttachment.Remove(a);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
