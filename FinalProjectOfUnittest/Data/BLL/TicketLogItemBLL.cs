using FinalProjectOfUnittest.Data.DAL;
using FinalProjectOfUnittest.Models;

namespace FinalProjectOfUnittest.Data.BLL
{
    public class TicketLogItemBLL
    {
        public TicketLogItemDAL ticketLogItemDAL;
        public TicketLogItemBLL(TicketLogItemDAL tld)
        {
            ticketLogItemDAL = tld;
        }

        public void Add(TicketLogItem item)
        {
            ticketLogItemDAL.Add(item);
        }
        public TicketLogItem GetById(int id)
        {
            return ticketLogItemDAL.Get(id);
        }

        public void Save()
        {
            ticketLogItemDAL.Save();
        }
    }
}
