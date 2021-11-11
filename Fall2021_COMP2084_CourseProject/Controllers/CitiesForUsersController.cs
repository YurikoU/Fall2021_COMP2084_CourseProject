using Fall2021_COMP2084_CourseProject.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fall2021_COMP2084_CourseProject.Controllers
{
    public class CitiesForUsersController : Controller
    {
        //Add DBContext object to use the database
        private readonly ApplicationDbContext _context;

        //Constructor that takes an instance of our DB connection object
        public CitiesForUsersController(ApplicationDbContext context)
        {
            //Assign the incoming DB connection so we can use it in any method in this controller
            _context = context;
        }


        //GET:  /CitiesForUsers
        public IActionResult Index()
        {
            //Use Cities Dbset to fetch list of cities to display to users
            var cities = _context.Cities.OrderBy(p => p.Province).ThenBy(p => p.Name).ToList(); //Convert the  query into the list   

            return View(cities);
        }


        //GET:  /CitiesForUsers/PostsByCity/{CityId}
        public IActionResult PostsByCity(int id)
        {
            //Get all posts under the selected city
            var posts = _context.Posts.Where(p => p.CityId == id).OrderBy(p => p.PostedDate).ToList();
            //Store the city name in ViewBag to display as a page title
            ViewBag.City = _context.Cities.Find(id).Name;
            return View(posts);
        }
    }
}
