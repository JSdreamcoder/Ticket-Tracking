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
            if (ta.Id == 0)
            {
                throw new ArgumentNullException("id cannot input 0");
            }
            else if (ta.ticket == null)
            {
                throw new ArgumentNullException("value of ticket cannot be null");
            }
            else
            {
                ticketAttachmentDAL.Add(ta);
            }
        }
        
        public void Save()
        {
            ticketAttachmentDAL.Save();
        }

    }
}
