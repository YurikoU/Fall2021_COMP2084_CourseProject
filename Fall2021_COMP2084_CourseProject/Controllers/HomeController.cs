using Fall2021_COMP2084_CourseProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Fall2021_COMP2084_CourseProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //Home page - Home
        public IActionResult Index()
        {
            return View();
        }

        //Privacy page - Home/Privacy
        public IActionResult Privacy()
        {
            return View();
        }


        //Post page - Home/Post
/*        public IActionResult Post()
        {
            return View();
        }*/


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
