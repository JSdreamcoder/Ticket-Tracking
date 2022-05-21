using FinalProjectOfUnittest.Data.BLL;
using FinalProjectOfUnittest.Data.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Unittest
{
    [TestClass]
    public class RoleBLTest
    {
        Mock<RoleDAL> repoMock;
        RoleBLL rolebll;
        RoleManager<IdentityRole> roleManager; 
        [TestInitialize]
        public void InitialLizeTest()
        {
            ICollection<IdentityRole> allroles = new List<IdentityRole>();
            allroles.Add(new IdentityRole("Submmiter"));
            allroles.Add(new IdentityRole("Developer"));
            allroles.Add(new IdentityRole("ProjectManager"));
            allroles.Add(new IdentityRole("Adminitrator"));
            repoMock = new Mock<RoleDAL>();
            
            repoMock.Setup(r => r.GetAll()).Returns(allroles);

            rolebll = new RoleBLL(repoMock.Object);
        }
        [TestMethod]
        public void TestMethod1()
        {
           //arrange
           
        }
    }
}