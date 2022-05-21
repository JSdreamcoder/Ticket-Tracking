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
        public void Add(Ticket newticket)
        {
            if (newticket.Title == null)
            {
                throw new ArgumentNullException("You must write title name");
            }else if (newticket.Description == null)
            {
                throw new ArgumentNullException("You must write descrition");
            }else
            {
                ticketDAL.Add(newticket);
            }
        }

        public ICollection<Ticket> GetAll()
        {
            return ticketDAL.GetAll(); 
        }

        public void Save()
        {
            ticketDAL.Save();
        }

    }
}
