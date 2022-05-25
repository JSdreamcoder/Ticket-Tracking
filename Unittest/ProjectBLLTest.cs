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
    public class ProjectBLLTest
    {

        Mock<ProjectDAL> repoMock;
        ProjectBLL projectBLL;
        Project newProject;
        ICollection<Project> allProjects;

        [TestInitialize]
        public void InitalizeTests()
        {
            newProject = new Project()
            {
                Id = 1,
                Name = "AnimalAL"

            };
            var newProject2 = new Project()
            {
                Id=2,
                Name = "HumaAI"
            };
            allProjects = new List<Project>() { newProject, newProject2 };
            repoMock = new Mock<ProjectDAL>();
            projectBLL = new ProjectBLL(repoMock.Object);
            repoMock.Setup(x => x.Get(It.Is<int>(i => i == 1))).Returns(newProject);
            repoMock.Setup(x => x.GetAll()).Returns(allProjects);

        }


        [TestMethod]
        public void GetReturnProjectWithInt1Argument()
        {
            //Arrange
            var inputProjectId = 1;

            //Act
            var result = projectBLL.GetById(inputProjectId);

            //Assert
            Assert.AreEqual(newProject, result);
        }

        [TestMethod]
        public void GetAllReturnAllProjects()
        {
            //Arrange
            var expected = allProjects;

            //Act 
            var result = projectBLL.GetAll();

            //Assert
            Assert.AreEqual(expected, result);

        }

     

    }
}
