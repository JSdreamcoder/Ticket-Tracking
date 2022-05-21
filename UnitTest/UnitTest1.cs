using FinalProjectOfUnittest.Data.BLL;
using FinalProjectOfUnittest.Data.DAL;
using FinalProjectOfUnittest.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        //Mock<AppUserDAL> AppUserMRepo;
        //AppUserBLL appUserBLL;

        //[TestInitialize]
        //public void InitializedTest()
        //{
        //    AppUserMRepo = new Mock<AppUserDAL>();

        //    appUserBLL=new AppUserBLL(AppUserMRepo.Object);

        //    AppUserMRepo.Setup(x=>x.Get(It.Is<int>(i=>i==7))).Returns(new AppUser
        //    {
        //        Email = "TestAppUser@gmail.com",
        //        NormalizedEmail = "TESTAPPUSER@GMAIL.COM",
        //        UserName = "TestAppUser@gmail.com",
        //        NormalizedUserName = "TESTAPPUSER@GMAIL.COM",
        //        EmailConfirmed = true,
        //        SecurityStamp = Guid.NewGuid().ToString()
        //    });
        //}

        //[TestMethod]
        //public void TestAppUser()
        //{
        //    Assert.AreEqual("TestAppUser@gmail.com", appUserBLL.GetTestAppUserBLL(7));
        //}

        Mock<IDAL<Ticket>> _ticket;
        TicketBLL _ticketBLL;


        [TestInitialize]
        public void InitializedTest()
        {
            _ticket = new Mock<IDAL<Ticket>>();
            _ticketBLL = new TicketBLL(_ticket.Object);
        }

    }
}