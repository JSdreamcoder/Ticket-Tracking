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
            ticketAttachmentDAL.Add(ta);
        }
        
        public void Save()
        {
            ticketAttachmentDAL.Save();
        }

    }
}
