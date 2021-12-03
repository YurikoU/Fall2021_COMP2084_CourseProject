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
        City invalidCity = new City
        {
            Id = -5,
            Province = "Alberta",
            Name = "Calgary",
        };
        City validCity = new City
        {
            Id = 111,
            Province = "Alberta",
            Name = "Calgary",
        };


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
        public void IndexReturnsValidCities()
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
        public void IndexReturnsValidView()
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
        public void EditReturns404IfCityidNull()
        {
            //Act(=when execution)
            var result = (ViewResult)controller.Edit(null)
                .Result;  //Allow you to read the asynchronous task and cast it to a view result, cuz the method(=Edit()) is async.

            //Assert(=result) 
            Assert.AreEqual("404", result.ViewName);
        }

        //Check for invalid CityId
        [TestMethod]
        public void EditReturns404IfCityidInvalid()
        {
            //Act(=when execution)
            var result = (ViewResult)controller.Edit(999).Result;//In-memory DB doesn't have CityId=999

            //Assert(=result) 
            Assert.AreEqual("404", result.ViewName);
        }

        //Check for returning the correct data model (City obj)
        [TestMethod]
        public void EditReturnsValidCityIfCityidValid()
        {
            //Act(=when execution)
            var result = (ViewResult)controller.Edit(cities[0].Id).Result;//In-memory DB has CityId=555(cities[0].Id)

            //Assert(=result) 
            Assert.AreEqual(cities[0], result.Model);
        }

        //Check for returning the correct view
        [TestMethod]
        public void EditReturnsValidViewIfCityidValid()
        {
            //Act(=when execution)
            var result = (ViewResult)controller.Edit(cities[0].Id).Result;

            //Assert(=result) 
            Assert.AreEqual("Edit", result.ViewName);

        }
        #endregion

        #region Create (GET)
        //Check for returning the correct view
        [TestMethod]
        public void CreateReturnsValidView()
        {
            //Act(=when execution)
            var result = (ViewResult)controller.Create(); //Create() method is NOT async.

            //Assert(=result) 
            Assert.AreEqual("Create", result.ViewName);
        }
        #endregion

        #region Delete (GET)
        //Check for null CityId
        [TestMethod]
        public void DeleteReturns404IfCityidNull()
        {
            //Act(=when execution)
            var result = (ViewResult)controller.Delete(null).Result;

            //Assert(=result) 
            Assert.AreEqual("404", result.ViewName);
        }

        //Check for invalid CityId
        [TestMethod]
        public void DeleteReturns404IfCityidInvalid()
        {
            //Act(=when execution)
            var result = (ViewResult)controller.Delete(-3).Result;

            //Assert(=result) 
            Assert.AreEqual("404", result.ViewName);
        }

        //Check for returning the correct data model (City obj)
        [TestMethod]
        public void DeleteReturnsValidCityIfCityidValid()
        {
            //Act(=when execution)
            var result = (ViewResult)controller.Delete(cities[0].Id).Result;

            //Assert(=result) 
            Assert.AreEqual(cities[0], result.Model);
        }

        //Check for returning the correct view
        [TestMethod]
        public void DeleteReturnsValidViewIfCityidValid()
        {
            //Act(=when execution)
            var result = (ViewResult)controller.Delete(cities[0].Id).Result;

            //Assert(=result) 
            Assert.AreEqual("Delete", result.ViewName);
        }
        #endregion

        #region DeleteConfirmed (POST)
        //Check for deleting the city
        [TestMethod]
        public void DeleteConfirmedDeletesCity()
        {
            //Act(=when execution)
            //Delete the city
            var result = controller.DeleteConfirmed(cities[0].Id).Result;

            //Return if the DB fails deleting the city            
            //Must be false as the DB can't find the city
            bool isCityExisting = _context.Cities.Any(c => c.Id == cities[0].Id);

            //Assert(=result) 
            Assert.IsFalse(isCityExisting);
        }

        //Check for NOT deleting another city
        [TestMethod]
        public void DeleteConfirmedNotDeleteAnotherCity()
        {
            //Act(=when execution)
            var before = _context.Cities.Count();
            var result = controller.DeleteConfirmed(cities[0].Id);
            var after = _context.Cities.Count();

            //Assert(=result) 
            Assert.AreEqual((before - 1), after);
        }

        //Check for redirecting to the proper view
        [TestMethod]
        public void DeleteConfirmedRedirectsValidView()
        {
            //Act(=when execution)
            //Cast result as RedirectToActionResult cuz the method to test returns that method
            var result = (RedirectToActionResult)controller.DeleteConfirmed(cities[0].Id).Result;

            //Assert(=result) 
            Assert.AreEqual("Index", result.ActionName);//asp-action
        }
        #endregion

        #region Create (POST)
        [TestMethod]
        public void CreateReturnsSameViewAgainIfInputInvalid()
        {
            //Act(=when execution)
            controller.ModelState.AddModelError("Id", "Id must be positive numbers only.");//Add an error
            var result = (ViewResult)controller.Create(invalidCity).Result;

            //Assert(=result) 
            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void CreateReturnsCityIfInputInvalid()
        {
            //Act(=when execution)
            controller.ModelState.AddModelError("Id", "Id must be positive numbers only.");//Add an error
            var result = (ViewResult)controller.Create(invalidCity).Result;

            //Assert(=result) 
            Assert.AreEqual(invalidCity, result.Model);
        }

        [TestMethod]
        public void CreateRedirectsValidViewIfInputValid()
        {
            //Act(=when execution)
            var result = (RedirectToActionResult)controller.Create(validCity).Result;

            //Assert(=result) 
            Assert.AreEqual("Index", result.ActionName);//asp-action
        }

        [TestMethod]
        public void CreateCreatesCity()
        {
            //Act(=when execution)
            var result = controller.Create(validCity);
            bool isCityCreated = _context.Cities.Any(c => c.Id == 111);

            //Assert(=result) 
            Assert.IsTrue(isCityCreated);
        }

        [TestMethod]
        public void CreateNotCreateAnotherCity()
        {
            //Act(=when execution)
            var before = _context.Cities.Count();
            var result = controller.Create(validCity);
            var after = _context.Cities.Count();

            //Assert(=result) 
            Assert.AreEqual((before + 1), after);
        }
        #endregion

        #region Edit (POST)
        [TestMethod]
        public void EditReturns404IfCityidNotMatched()
        {
            //Act(=when execution)
            var result = (ViewResult)controller.Edit(cities[2].Id, cities[0]).Result;

            //Assert(=result) 
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void EditReturns404IfCityidNotExist()
        {
            //Act(=when execution)
            var result = (ViewResult)controller.Edit(11111, cities[0]).Result;


            //Assert(=result) 
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void EditReturnsSameViewAgainIfInputInvalid()
        {
            //Act(=when execution)
            controller.ModelState.AddModelError("Id", "Id must be positive numbers only.");//Add an error
            var result = (ViewResult)controller.Edit(invalidCity.Id, invalidCity).Result;

            //Assert(=result) 
            Assert.AreEqual("Edit", result.ViewName);
        }

        [TestMethod]
        public void EditReturnsCityIfInputInvalid()
        {
            //Act(=when execution)
            controller.ModelState.AddModelError("Id", "Id must be positive numbers only.");//Add an error
            var result = (ViewResult)controller.Edit(invalidCity.Id, invalidCity).Result;

            //Assert(=result) 
            Assert.AreEqual(invalidCity, result.Model);
        }

        [TestMethod]
        public void EditRedirectsValidViewIfInputValid()
        {
            //Act(=when execution)
            var result = (RedirectToActionResult)controller.Edit(555, cities[0]).Result;

            //Assert(=result) 
            Assert.AreEqual("Index", result.ActionName);//asp-action
        }
        #endregion
    }
}
