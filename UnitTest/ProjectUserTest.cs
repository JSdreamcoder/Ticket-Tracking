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
    public class ProjectUserBLLTest
    {
        Mock<ProjectUserDAL> repoMock;
        ProjectUserBLL projectUserBLL;
        ProjectUser newProjectUser;
        ProjectUser newProjectUser2;
        ICollection<ProjectUser> allProjectUser;
        [TestInitialize]
        public void InitalizeTests()
        {
            
            allProjectUser = new List<ProjectUser>() { newProjectUser };
            repoMock = new Mock<ProjectUserDAL>();
            projectUserBLL = new ProjectUserBLL(repoMock.Object);
            
        }
       
        [TestMethod]
        public void GetFuncThrowExceptionIdOruserIdIsNull()
        {
            //Arrange
            newProjectUser = new ProjectUser
            {
                Id = 0,
                ProjectId = 1,
                UserId = "aaa",
            };
            newProjectUser2 = new ProjectUser
            {
                Id = 1,
                ProjectId = 1,
                UserId = null,
            };
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => projectUserBLL.Add(newProjectUser));
            Assert.ThrowsException<ArgumentNullException>(() => projectUserBLL.Add(newProjectUser2));
        }
    }
}