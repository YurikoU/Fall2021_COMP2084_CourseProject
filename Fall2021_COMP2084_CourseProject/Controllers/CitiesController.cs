using Microsoft.AspNetCore.Mvc;

namespace Fall2021_COMP2084_CourseProject.Controllers
{
    public class CitiesController : Controller
    {
        //Cities
        public IActionResult Index()
        {
            //Load the view to display
            return View();
        }

        //Cities/Browse?city={asp-route-city value}
        public IActionResult Browse(string city) 
        {
            //Store the clicked city name
            ViewBag.city = city;

            //Load the view to display
            return View();
        }
    }
}
