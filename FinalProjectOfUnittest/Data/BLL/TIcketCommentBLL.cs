using FinalProjectOfUnittest.Data.DAL;
using FinalProjectOfUnittest.Models;

namespace FinalProjectOfUnittest.Data.BLL
{
    public class TIcketCommentBLL 
    {
        public TicketCommentDAL ticketCommentDAL { get; set; }
        public TIcketCommentBLL(TicketCommentDAL td)
        {
            ticketCommentDAL = td;
        }

        public virtual void Add(TicketComment tc)
        {
         
            if (tc.Comment == "")
            {
                throw new ArgumentNullException("You must write a comment");
            }
            else
            {
                ticketCommentDAL.Add(tc);
            }
        }
        public virtual ICollection<TicketComment> GetAll()
        {
            return ticketCommentDAL.GetAll();
        }

        public virtual TicketComment Get(int Id)
        {
            return ticketCommentDAL.Get(Id);
        }

        public void Update(TicketComment tc)
        {
            ticketCommentDAL.Update(tc);
        }
        public void Save()
        {
            ticketCommentDAL.Save();
        }

        public void Delete(TicketComment tc)
        {
            ticketCommentDAL.Delete(tc);
        }

        

   
    }
}
