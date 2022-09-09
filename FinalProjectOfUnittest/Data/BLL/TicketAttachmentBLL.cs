using FinalProjectOfUnittest.Data.DAL;
using FinalProjectOfUnittest.Models;

namespace FinalProjectOfUnittest.Data.BLL
{
    public class TicketAttachmentBLL
    {
        TicketAttachmentDAL ticketAttachmentDAL;
        public TicketAttachmentBLL(TicketAttachmentDAL td)
        {
            ticketAttachmentDAL = td;
        }

        public void Add(TicketAttachment ta)
        {
            
            if (ta.TicketId == 0)
            {
                throw new ArgumentNullException("ticket cannot be 0");
            }
            else
            {
                ticketAttachmentDAL.Add(ta);
            }
        }
        
        public TicketAttachment GetById(int id)
        {
            return ticketAttachmentDAL.Get(id);
        }

        public void Save()
        {
            ticketAttachmentDAL.Save();
        }

    }
}
