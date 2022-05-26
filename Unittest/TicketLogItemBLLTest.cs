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
    public class TicketLogItemBLLTest
    {

        Mock<TicketLogItemDAL> repoMock;
        TicketLogItemBLL ticketLogItemBLL;
        TicketLogItem newTicketLogItem;
        ICollection<TicketLogItem> allTicketLogItem;

        [TestInitialize]
        public void InitalizeTests()
        {
            newTicketLogItem = new TicketLogItem
            {
                Id = 1,
                TicketHistoryId = 1,
                Title = "",
                Description = "",
                Created = DateTime.Now,
                ProjectId = 1,

            };

            allTicketLogItem = new List<TicketLogItem>() { newTicketLogItem };
            repoMock = new Mock<TicketLogItemDAL>();
            ticketLogItemBLL = new TicketLogItemBLL(repoMock.Object);


        }

        public void InitaliseTests()
        {
            repoMock = new Mock<TicketLogItemDAL>();
            ticketLogItemBLL = new TicketLogItemBLL(repoMock.Object);

            repoMock.Setup(x => x.Get(It.Is<int>(i => i == 1))).Returns(newTicketLogItem);
        }
        [TestMethod]

        public void GetFuncThrowExceptionTicketLogItemTitleAndDescriptionTest()
        {
            //Arrange
            newTicketLogItem = new TicketLogItem
            {
                Id = 1,
                TicketHistoryId = 1,
                Title = "",
                Description = "",
                Created = DateTime.Now,
                ProjectId = 1,

            };
            //Act & Assert

            Assert.ThrowsException<ArgumentNullException>(() => ticketLogItemBLL.Add(newTicketLogItem));

        }

    }
}
