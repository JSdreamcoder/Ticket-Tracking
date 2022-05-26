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
    public class TicketNotificationBLLTest
    {

        Mock<TicketNotificationDAL> repoMock;
        TicketNotificationBLL ticketNotificationBLL;
        TicketNotification newTicketNotification;
        ICollection<TicketNotification> allTicketNotification;

        [TestInitialize]
        public void InitalizeTests()
        {
            newTicketNotification = new TicketNotification
            {
                Id = 1,
                UserId = "1",
                TicketId = 1,
            };

            allTicketNotification = new List<TicketNotification>() { newTicketNotification };
            repoMock = new Mock<TicketNotificationDAL>();
            ticketNotificationBLL = new TicketNotificationBLL(repoMock.Object);


        }

        public void InitaliseTests()
        {
            repoMock = new Mock<TicketNotificationDAL>();
            ticketNotificationBLL = new TicketNotificationBLL(repoMock.Object);

            repoMock.Setup(x => x.Get(It.Is<int>(i => i == 1))).Returns(newTicketNotification);
        }
        [TestMethod]

        public void GetFuncThrowExceptionTicketNotification()
        {
            //Arrange
            newTicketNotification = new TicketNotification
            {
                Id = 1,
                UserId = "1",
                TicketId = 1,
                
            };
            //Act & Assert

            Assert.ThrowsException<ArgumentNullException>(() => ticketNotificationBLL.Add(newTicketNotification));

        }

    }
}
