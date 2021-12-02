using Fall2021_COMP2084_CourseProject.Controllers;
using Fall2021_COMP2084_CourseProject.Data;
using Fall2021_COMP2084_CourseProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchInRoomTests
{
    //[TestMethod] decorator: designed for only the test class
    [TestClass]
    public class CitiesControllerTests
    {
        #region Class level variables and methods
        private ApplicationDbContext _context;
        CitiesController controller;
        List<City> cities = new List<City>();//Mock data to run test methods

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

            //Populate the DB with mock data
            cities.Add(new City
            {
                Id = 555,
                Province = "British Columbia",
                Name = "Vancouver",
            });
            cities.Add(new City
            {
                Id = 123,
                Province = "British Columbia",
                Name = "Barrie",
            });
            cities.Add(new City
            {
                Id = 17,
                Province = "British Columbia",
                Name = "Ottawa",
            });
            foreach (var city in cities)
            {
                //Add each mock product to store them into the in-memory DB
                _context.Cities.Add(city);
            }
            //Commit the mock data to the in-memory DB
            _context.SaveChanges();

            //Instantiate the controller with the DB dependency
            //CitiesController can't be instantiated without an active DB as the controller starts with SQL DB connection before running methods
            controller = new CitiesController(_context);
        }
        #endregion

        #region Index (GET)
        //Check for returning the correct data model (City obj)
        [TestMethod]
        public void IndexLoadCities()
        {
            //Act(=when execution)
            var result = (ViewResult)controller.Index().Result;

            //Take the data model in the ViewResult, and then cast it
            List<City> dataModel = (List<City>)result.Model;

            //Assert(=result) 
            //Compare the mock data and the real data model
            CollectionAssert.AreEqual(cities.OrderBy(c => c.Province).ThenBy(c => c.Name).ToList(), dataModel);
        }

        //Check for returning the correct view
        [TestMethod]
        public void IndexLoadsView()
        {
            //Act(=when execution)
            var result = (ViewResult)controller.Index().Result;

            //Assert(=result) 
            Assert.AreEqual("Index", result.ViewName);
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
            var result = (ViewResult)controller.Edit(999).Result;//In-memory DB doesn't have CityId=999

            //Assert(=result) 
            Assert.AreEqual("404", result.ViewName);
        }

        //Check for returning the correct data model (City obj)
        [TestMethod]
        public void EditLoadsCityIfCityidValid()
        {
            //Act(=when execution)
            var result = (ViewResult)controller.Edit(cities[0].Id).Result;//In-memory DB has CityId=555(cities[0].Id)

            //Assert(=result) 
            Assert.AreEqual(cities[0], result.Model);
        }

        //Check for returning the correct view
        [TestMethod]
        public void EditLoadsViewIfCityidValid()
        {
            //Act(=when execution)
            var result = (ViewResult)controller.Edit(cities[0].Id).Result;

            //Assert(=result) 
            Assert.AreEqual("Edit", result.ViewName);

        }
        #endregion

        #region Create (GET)


        #endregion

    }
}
