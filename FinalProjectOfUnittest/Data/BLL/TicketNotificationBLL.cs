using FinalProjectOfUnittest.Data.DAL;
using FinalProjectOfUnittest.Models;

namespace FinalProjectOfUnittest.Data.BLL
{

    public class TicketNotificationBLL
    {
        public TicketNotificationDAL ticketNotificationDAL;
        public TicketNotificationBLL(TicketNotificationDAL tnd)
        {
            this.ticketNotificationDAL = tnd;
        }
        public void Add(TicketNotification entity)
        {
            if (entity.Id == 0)
            {
                throw new ArgumentNullException("id cannot input 0");
            }
            else if (entity.Ticket == null)
            {
                throw new ArgumentNullException("value of ticket cannot be null");
            }
            else
            {
                ticketNotificationDAL.Add(entity);
            }
        }
        public TicketNotification Get(Func<TicketNotification, bool> firstFuction)
        {
            return ticketNotificationDAL.Get(firstFuction);
        }
        public ICollection<TicketNotification> GetAll()
        {
            return ticketNotificationDAL.GetAll();
        }
        public ICollection<TicketNotification> GetList(Func<TicketNotification, bool> whereFuction)
        {
            return ticketNotificationDAL.GetList(whereFuction);
        }

        public void Update(TicketNotification entity)
        {
            ticketNotificationDAL.Update(entity);
        }
        public void Save()
        {
            ticketNotificationDAL.Save();
        }
    }
}
