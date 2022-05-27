using FinalProjectOfUnittest.Models;

namespace FinalProjectOfUnittest.Data.DAL
{
    public class TicketCommentDAL : IDAL<TicketComment>
    {
        public ApplicationDbContext Context { get; set; }
        public TicketCommentDAL(ApplicationDbContext context)
        {
            Context = context;
        }

        //Create
        public void Add(TicketComment appuser)
        {
            Context.Add(appuser);
        }

        //Read
        public TicketComment Get(int id)
        {
            return Context.TicketComment.First(a => a.Id == id);
        }
        public TicketComment Get(Func<TicketComment, bool> firstFuction)
        {
            return Context.TicketComment.First(firstFuction);

        }
        public ICollection<TicketComment> GetAll()
        {
            return Context.TicketComment.ToList();
        }
        public ICollection<TicketComment> GetList(Func<TicketComment, bool> func)
        {
            return Context.TicketComment.Where(func).ToList();
        }

        //Update
        public void Update(TicketComment a)
        {
            Context.TicketComment.Update(a);
        }

        public void Delete(TicketComment a)
        {
            Context.TicketComment.Remove(a);
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public TicketCommentDAL()
        {

        }
    }
}
