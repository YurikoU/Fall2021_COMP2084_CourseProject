using Microsoft.AspNetCore.Mvc;

namespace Fall2021_COMP2084_CourseProject.Controllers
{
    public class CitiesController : Controller
    {
        /*private const string DateFormat = "yyyy-MM-dd";*/

        //Cities
        public IActionResult Index()
        {
/*            var barrie = new List<Barrie>();
            var hamilton = new List<Hamilton>();
            var kitchener = new List<Kitchener>();
            var ottawa = new List<Ottawa>();
            var toronto = new List<Toronto>();*/
/*            var cities = new List<City>();

            for (var i = 1; i <= 5; i++)
            {
                cities.Add(new City() { Id = i });
            }
*/
            //Pass the cities list to the view
            return View();
        }


        //Cities/Browse?city={asp-route-city value}
        public IActionResult Browse(string city) 
        {
            //Make sure if "city" parameter has any value
            //If it's null, the user will be forced to bring back to Index.cshtml to click a city properly
            if (city == null)
            {
                return RedirectToAction("Index");
            }

            //Store the clicked city name
            ViewBag.city = city;

            //Load the view to display
            return View();
        }

        //Cities/Create
        public IActionResult Create() 
        {
            return View();
        }
    }
}
