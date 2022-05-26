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
            if (th.Id == 0)
            {
                throw new ArgumentNullException("id cannot input 0");
            }
            else if (th.Ticket == null)
            {
                throw new ArgumentNullException("value of ticket cannot be null");
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
