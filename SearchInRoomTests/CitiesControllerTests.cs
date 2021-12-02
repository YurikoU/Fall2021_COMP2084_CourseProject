using Fall2021_COMP2084_CourseProject.Controllers;
using Fall2021_COMP2084_CourseProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SearchInRoomTests
{
    //[TestMethod] decorator: designed for only the test class
    [TestClass]
    public class CitiesControllerTests
    {
        #region Class level variables and methods
        private ApplicationDbContext _context;
        CitiesController controller;

        //[TestInitialize] decorator: runs automatically before each test
        [TestInitialize]
        public void TestInitialize()
        {
            //Setting-up the options
            //Create in-memory DB not to connect to external DB
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())//To use UseInMemoryDatabase(), you must have a unique name, eg; using Guid().
                .Options;

            //Instantiate the DB object
            _context = new ApplicationDbContext(options);

            //Instantiate the controller with the DB dependency
            //CitiesController can't be instantiated without an active DB as the controller starts with SQL DB connection before running methods
            controller = new CitiesController(_context);
        }
        #endregion


        #region Edit (GET)
        //[TestMethod] decorator: designed for a unit test method
        [TestMethod]
        //Check for null CityId
        public void EditLoads404IfCityidNull()
        {
            //Act(=when execution)
            var result = (ViewResult)controller.Edit(null)
                .Result;  //Allow you to read the asynchronous task and cast it to a view result, cuz the method(=Edit()) is async.

            //Assert(=result) 
            Assert.AreEqual("404", result.ViewName);
        }

        //Check for invalid CityId
        [TestMethod]
        public void EditLoads404IfCityidInvalid()
        {
            //Act(=when execution)
            var result = (ViewResult)controller.Edit(999).Result;//DB doesn't have CityId=999

            //Assert(=result) 
            Assert.AreEqual("404", result.ViewName);
        }

        //Check for returning the correct data model (City obj)
        [TestMethod]
        public void EditLoadsCityIfCityidValid()
        {
            //Arrange(=given values)


            //Act(=when execution)


            //Assert(=result) 

        }

        //Check for returning the correct view
        [TestMethod]
        public void EditLoadsViewIfCityidValid()
        {
            //Arrange(=given values)


            //Act(=when execution)

            //Assert(=result) 

        }
        #endregion
    }
}
