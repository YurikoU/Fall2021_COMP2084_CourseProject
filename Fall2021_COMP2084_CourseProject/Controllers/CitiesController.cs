using Microsoft.AspNetCore.Mvc;

namespace Fall2021_COMP2084_CourseProject.Controllers
{
    public class CitiesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
