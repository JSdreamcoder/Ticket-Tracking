using FinalProjectOfUnittest.Data.BLL;
using FinalProjectOfUnittest.Data.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

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
            allroles.Add(new IdentityRole("Administrator"));
            repoMock = new Mock<RoleDAL>();
            
            repoMock.Setup(r => r.GetAll()).Returns(allroles);

            rolebll = new RoleBLL(repoMock.Object);
        }
        [TestMethod]
        public void GetCounterPartRoles()
        {
           //arrange
           IList<string> assignedRoles = new List<string>();
            assignedRoles.Add("Submmiter");
            assignedRoles.Add("Developer");
            List<IdentityRole> expectedList = new List<IdentityRole>();
                expectedList.Add(rolebll.GetAllRoles().First(r=>r.Name == "ProjectManager"));
                expectedList.Add(rolebll.GetAllRoles().First(r => r.Name == "Administrator"));

            //act
            var counterroles = rolebll.GetCounterPartRoles(assignedRoles);

            //assert
            Assert.IsTrue(counterroles.Contains(expectedList[0]));
            Assert.IsTrue(counterroles.Contains(expectedList[1]));


        }

        public void userrolesIsNullReturnExeception()
        {

        }
    }
}