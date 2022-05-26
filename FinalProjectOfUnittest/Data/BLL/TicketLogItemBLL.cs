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
            if (item.Title == "")
            {
                throw new ArgumentNullException("You must write title name");
            }
            else if (item.Description == "")
            {
                throw new ArgumentNullException("You must write descrition");
            }
            else
            {
                ticketLogItemDAL.Add(item);
            }
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
