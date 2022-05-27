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


   
    public class TicketCommentBLLTest
    {

        Mock<TicketCommentDAL> repoMock;
        TIcketCommentBLL ticketCommentBLL;
        TicketComment newTicketComment;
        ICollection<TicketComment> allTicketComment;

        [TestInitialize]
        public void InitalizeTests()
        {
            newTicketComment = new TicketComment
            {
                Id = 1,
                Comment = "",
                Created = DateTime.Now,
                UserId =  "1",
                TicketId = 1,
            };

            allTicketComment = new List<TicketComment>() { newTicketComment };
            repoMock = new Mock<TicketCommentDAL>();
            ticketCommentBLL = new TIcketCommentBLL(repoMock.Object);
            

        }

        public void InitaliseTests()
        {
            repoMock = new Mock<TicketCommentDAL>();
            ticketCommentBLL = new TIcketCommentBLL(repoMock.Object);

            repoMock.Setup(x => x.Get(It.Is<int>(i => i == 1))).Returns(newTicketComment);
        }
        [TestMethod]

        public void GetFuncThrowExceptionCommentIsEmptyAndIdIsZero()
        {
            //Arrange
            newTicketComment = new TicketComment
            {
                Id = 1,
                Comment = "",
                Created = DateTime.Now,
                UserId = "1",
                TicketId = 1,
            };
            //Act
            //Assert
            // Assert.AreEqual()
            Assert.ThrowsException<ArgumentNullException>(() => ticketCommentBLL.Add(newTicketComment));

        }






    }
}
