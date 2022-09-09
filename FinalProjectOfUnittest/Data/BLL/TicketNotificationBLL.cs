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
            
            if (entity.TicketId == 0)
            {
                throw new ArgumentNullException("ticketId cannot be 0");
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

        public void Remove(TicketNotification t)
        {
            
            ticketNotificationDAL.Delete(t);
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
