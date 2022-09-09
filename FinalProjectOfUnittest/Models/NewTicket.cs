namespace FinalProjectOfUnittest.Models
{
    public class NewTicket
    {
        public abstract class Ticket//using abstract set parent class
        {
            //properties 
            public int Id;
            public string name;
            public TicketType TicketType;
            public int ReponseDeadLine;
            public int BreachDeadLine;
            public DeadLineStrategy DeadLineStrategy;//declare strategy in main class
            //outside of main calss caused errors...
            public void DeadLineSetting(DeadLineStrategy ds)//can be used in children 
            {
                DeadLineStrategy = ds;
            }
        }
        public enum TicketType//enum is a letters string,no numbers inside
        {
            bug,
            payment,
            ServiceRequest,
        }
        public class Bugreport : Ticket//Bugreport inherits from Ticket
        {
            public string ErrorCode;
            public string ErrorLog;
        }
        public class ServiceRequest : Ticket //ServiceRequest inherits from Ticket
        {
            public ServiceRequestType type;//setting type
        }
        public enum ServiceRequestType
        {
            RegularService,
            PriorityService,
        }


        public interface DeadLineStrategy//using interface Strategy Pattern,
                                         //set DeadLineStrategy as parent
        {
            public void SetDeadLine(Ticket ticket);
        }
        public class InitialSetting : DeadLineStrategy//inherit
        {
            public void SetDeadLine(Ticket ticket)
            {
                ticket.ReponseDeadLine = 100;
                ticket.BreachDeadLine = 50;
            }
        }
        public class FormalSetting : DeadLineStrategy//inherit
        {
            public void SetDeadLine(Ticket ticket)
            {
                ticket.ReponseDeadLine = 12;
                ticket.BreachDeadLine = 6;
            }
        }

        public class CreateTicket // Factory Pattern,this case using Create func,
                                  // this condition shoud be type
                                  //learned from Beverage Factory exercise
        {

            public Ticket Create(int id, string name, TicketType type, string errorcode, string errorlog, ServiceRequestType servicetype)
            {
                //added ErrorCodeAndLogAndServiceRequest into property of create func
                if (type == TicketType.bug)//depends on the type, selcet deadline setting
                {
                    var newbugreport = new Bugreport() { Id = id, name = name, TicketType = type, ErrorCode = errorcode, ErrorLog = errorlog };
                    newbugreport.DeadLineSetting(new FormalSetting());//set deadline func
                    return newbugreport;
                }
                else if (type == TicketType.ServiceRequest)
                {
                    var newservice = new ServiceRequest() { Id = id, name = name, TicketType = type, type = servicetype };
                    newservice.DeadLineSetting(new InitialSetting());
                    return newservice;
                }
                //else if (type = "other children(payment etc.)")
                else
                {
                    throw new Exception("Error");
                }
            }
        }
    }
}