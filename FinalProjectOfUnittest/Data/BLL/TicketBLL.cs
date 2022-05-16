using FinalProjectOfUnittest.Data.DAL;
using FinalProjectOfUnittest.Models;

namespace FinalProjectOfUnittest.Data.BLL
{
    public class TicketBLL
    {
        TicketDAL ticketDAL;
        public TicketBLL(TicketDAL td)
        {
            ticketDAL = td;
        }

        public ICollection<Ticket> GetAll()
        {
            return ticketDAL.GetAll(); 
        }

    }
}
