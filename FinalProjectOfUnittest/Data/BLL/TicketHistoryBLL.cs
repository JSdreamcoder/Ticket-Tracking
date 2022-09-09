using FinalProjectOfUnittest.Data.DAL;
using FinalProjectOfUnittest.Models;

namespace FinalProjectOfUnittest.Data.BLL
{
    public class TicketHistoryBLL
    {
        public TicketHistoryDAL ticketHistoryDAL;
        public TicketHistoryBLL(TicketHistoryDAL thd)
        {
            ticketHistoryDAL = thd;
        }

        public void Add(TicketHistory th)
        {
           
            if (th.TicketId == 0)
            {
                throw new ArgumentNullException("Ticket Id cannot be 0");
            }
            else
            {
                ticketHistoryDAL.Add(th);
            }
        }
        public ICollection<TicketHistory> GetALL()
        {
            return ticketHistoryDAL.GetAll();   
        }
        public ICollection<TicketHistory> GetList(Func<TicketHistory,bool> func)
        {
            return ticketHistoryDAL.GetList(func);
        }

        public void Save()
        {
            ticketHistoryDAL.Save();
        }
    }
}
