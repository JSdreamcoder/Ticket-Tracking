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
            ticketCommentDAL.Add(tc);
        }

        public void Save()
        {
            ticketCommentDAL.Save();
        }
    }
}
