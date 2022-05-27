using FinalProjectOfUnittest.Data.BLL;
using FinalProjectOfUnittest.Data.DAL;
using FinalProjectOfUnittest.Models;
using Microsoft.AspNetCore.Identity;
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
    public class AppUserBLLTest
    {
        Mock<AppUserDAL> repoMock;
        AppUserBLL appUserBLL;
        AppUser user1;
        AppUser user2;
        ICollection<AppUser> appUsers;

        [TestInitialize]
        public void InitalizeTests()
        {
            user1 = new AppUser()
            {
                Email = "Administrator@mitt.ca",
                NormalizedEmail = "ADMINISTRATOR@MITT.CA",
                UserName = "Administrator@mitt.ca",
                NormalizedUserName = "ADMINISTRATOR@MITT.CA",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            user2 = new AppUser()
            {
                Email = "Projectmanager@mitt.ca",
                NormalizedEmail = "PROJECTMANAGER@MITT.CA",
                UserName = "Projectmanager@mitt.ca",
                NormalizedUserName = "PROJECTMANAGER@MITT.CA",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            appUsers = new List<AppUser>() { user1, user2 };
          
            repoMock = new Mock<AppUserDAL>();
            appUserBLL = new AppUserBLL(repoMock.Object);
            repoMock.Setup(x => x.GetAll()).Returns(appUsers);

        }


        [TestMethod]
        public void GetByUserIdThrowExceptionWhenIdIsNull()
        {
            //Arrange
            string Id = null;

            Assert.ThrowsException<Exception>(() => appUserBLL.GetUserbyId(Id));
        }

        [TestMethod]
        public void GetAllReturnAllappUsers()
        {
            //Arrange
            string expectedUserName1 = "Administrator@mitt.ca";
            string expectedUserName2 = "Projectmanager@mitt.ca";

            //Atc
            var Actresult = appUserBLL.GetAllUsers().ToList();

            //Assert
            Assert.AreEqual(expectedUserName1, Actresult[0].UserName);
            Assert.AreEqual(expectedUserName2, Actresult[1].UserName);
        }




    }
}
