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
    public class TicketAttachmentBLLTest
    {

        Mock<TicketAttachmentDAL> repoMock;
        TicketAttachmentBLL ticketAttachmentBLL;
        TicketAttachment newTicketAttachment;
        ICollection<TicketAttachment> allTicketAttachment;

        [TestInitialize]
        public void InitalizeTests()
        {
            newTicketAttachment = new TicketAttachment
            {
                Id = 1,
                FilePath = "string>",
                Description = "string>",
                Created = DateTime.Now,
                UserId = "1",
                TicketId = 1,
            };

            allTicketAttachment = new List<TicketAttachment>() { newTicketAttachment };
            repoMock = new Mock<TicketAttachmentDAL>();
            ticketAttachmentBLL = new TicketAttachmentBLL(repoMock.Object);


        }

        public void InitaliseTests()
        {
            repoMock = new Mock<TicketAttachmentDAL>();
            ticketAttachmentBLL = new TicketAttachmentBLL(repoMock.Object);

            repoMock.Setup(x => x.Get(It.Is<int>(i => i == 1))).Returns(newTicketAttachment);
        }
        [TestMethod]

        public void GetFuncThrowExceptionTicketAttachmentIdIsZeroOrNot()
        {
            //Arrange
            newTicketAttachment = new TicketAttachment
            {
                Id = 1,
                FilePath = "",
                Description = "",
                Created = DateTime.Now,
                UserId = "1",
                TicketId = 1,
            };
            //Act
            //Assert
            // Assert.AreEqual()
            Assert.ThrowsException<ArgumentNullException>(() => ticketAttachmentBLL.Add(newTicketAttachment));

        }






    }
}
