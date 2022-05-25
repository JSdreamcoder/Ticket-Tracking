using FinalProjectOfUnittest.Data.BLL;
using FinalProjectOfUnittest.Data.DAL;
using FinalProjectOfUnittest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unittest
{
    [TestClass]
    public class TicketBLTest
    {
        Mock<TicketDAL> repoMock;
        TicketBLL ticketBLL;
        string initialBalance = "Title Use it to Text";
        Ticket newticket;

        [TestInitialize]
        public void InitalizeTests()
        {
            repoMock = new Mock<TicketDAL>();
            ticketBLL = new TicketBLL(repoMock.Object);

            repoMock.Setup(x => x.Get(It.Is<int>(i => i == 1))).Returns(newticket);
        }
           
       
        [TestMethod]
        public void GetFunctionThrowExceptionWithEmptyDescription()
        {
            //Arrange
            newticket = new Ticket
            {
                Id = 1,
                Title = "Title Use it to Text",
                Description = "",
                Created = DateTime.Now,
                TicketType = TicketTypes.BugReport,
                TicketPriority = TicketPriorities.High,
                TicketStatus = TicketStatus.Assigned,
                ProjectId = 1
                
            };

            // Act Assert
            Assert.ThrowsException<ArgumentNullException>(() => ticketBLL.Add(newticket));
        }

        [TestMethod]
        public void GetFunctionThrowExceptionWithEmptyTitle()
        {
            newticket = new Ticket
            {
                Id = 1,
                Title = "",
                Description = "Description Use it to Text",
                Created = DateTime.Now,
                TicketType = TicketTypes.BugReport,
                TicketPriority = TicketPriorities.High,
                TicketStatus = TicketStatus.Assigned,
                ProjectId = 1

            };
            Assert.ThrowsException<ArgumentNullException>(() => ticketBLL.Add(newticket));
        }
       
    }
}
