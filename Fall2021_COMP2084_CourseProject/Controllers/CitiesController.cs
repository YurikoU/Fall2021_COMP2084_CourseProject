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

        //Cities/Browse
        public IActionResult Browse() 
        {
            //Load the view to display
            return View();
        }
    }
}
