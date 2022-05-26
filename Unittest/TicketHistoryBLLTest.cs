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
    public class TicketHistoryBLLTest
    {

        Mock<TicketHistoryDAL> repoMock;
        TicketHistoryBLL ticketHistoryBLL;
        TicketHistory newTicketHistory;
        ICollection<TicketHistory> allTicketHistory;

        [TestInitialize]
        public void InitalizeTests()
        {
            newTicketHistory = new TicketHistory
            {
                Id = 1,
                Property = "",
                Changed = DateTime.Now,
                UserId = "1",
                TicketId = 1,
            };

            allTicketHistory = new List<TicketHistory>() { newTicketHistory };
            repoMock = new Mock<TicketHistoryDAL>();
            ticketHistoryBLL = new TicketHistoryBLL(repoMock.Object);


        }

        public void InitaliseTests()
        {
            repoMock = new Mock<TicketHistoryDAL>();
            ticketHistoryBLL = new TicketHistoryBLL(repoMock.Object);

            repoMock.Setup(x => x.Get(It.Is<int>(i => i == 1))).Returns(newTicketHistory);
        }
        [TestMethod]

        public void GetFuncThrowExceptionTicketHistoryTest()
        {
            //Arrange
            newTicketHistory = new TicketHistory
            {
                Id = 1,
                Property = "",
                Changed = DateTime.Now,
                UserId = "1",
                TicketId = 1,
            };
            //Act & Assert

            Assert.ThrowsException<ArgumentNullException>(() => ticketHistoryBLL.Add(newTicketHistory));

        }

    }
}
