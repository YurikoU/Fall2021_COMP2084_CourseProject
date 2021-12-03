using Fall2021_COMP2084_CourseProject.Data;
using Fall2021_COMP2084_CourseProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
/*
GET : Before clicking the submit button
POST: After submitting the form
*/


namespace Fall2021_COMP2084_CourseProject.Controllers
{
    //Only the Administrator account can accesses
    [Authorize(Roles = "Administrator")]
    public class CitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CitiesController(ApplicationDbContext context)
        {
            //Process the database connection
            _context = context;
        }

        // GET: Cities
        public async Task<IActionResult> Index()
        {
            //Retrieve the data and convert it to the list
            return View("Index", await _context.Cities.OrderBy(p => p.Province).ThenBy(p => p.Name).ToListAsync());
        }

        // GET: Cities/Details/5
        [AllowAnonymous]//Make this Details() method public, overriding [Authorize] of CitiesController class
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View("404");
            }

            var city = await _context.Cities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (city == null)
            {
                return View("404");
            }

            return View(city);
        }

        // GET: Cities/Create
        public IActionResult Create()
        {
            return View("Create");
        }

        // POST: Cities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AddedDate,Province,Name")] City city)
        {
            City sameCityName = _context.Cities.FirstOrDefault(p => p.Name == city.Name);
            City sameProvinceName = _context.Cities.FirstOrDefault(p => p.Province == city.Province);
            if (sameCityName != null && sameProvinceName != null)
            {
                //Alert when the same city already exists
                ModelState.AddModelError("cityNameAlert", "The city already exists.");
            }

            //Check if the input is valid
            if (ModelState.IsValid)
            {
                //Add the City object
                _context.Add(city);

                //PostedDate is today's date
                city.AddedDate = System.DateTime.Now;

                //Save the changes on the database connection
                await _context.SaveChangesAsync();

                //Once done, it will redirect the user to the index page
                return RedirectToAction(nameof(Index));
            }
            return View("Create", city);
        }

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return View("404");
            }

            var city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return View("404");
            }
            return View("Edit", city);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AddedDate,Province,Name")] City city)
        {

            if (id != city.Id)
            {
                return View("404");
            }

            City sameCityName = _context.Cities.FirstOrDefault(p => p.Name == city.Name);
            City sameProvinceName = _context.Cities.FirstOrDefault(p => p.Province == city.Province);
            if (sameCityName != null && sameProvinceName != null)
            {
                //Alert when the same city already exists
                ModelState.AddModelError("cityNameAlert", "The city already exists.");
            }

            //Check if the input is valid
            if (ModelState.IsValid)
            {
                try
                {
                    //Update the City object
                    _context.Update(city);

                    //Save the changes on the database connection
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityExists(city.Id))
                    {
                        return View("404");
                    }
                    else
                    {
                        throw;
                    }
                }
                //Once done, it will redirect the user to the index page
                return RedirectToAction(nameof(Index));
            }
            return View("Edit", city);
        }

        // GET: Cities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View("404");
            }

            var city = await _context.Cities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (city == null)
            {
                return View("404");
            }

            return View("Delete", city);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var city = await _context.Cities.FindAsync(id);

            //Delete the City object
            _context.Cities.Remove(city);

            //Save the changes on the database connection
            await _context.SaveChangesAsync();

            //Once done, it will redirect the user to the index page
            return RedirectToAction(nameof(Index));
        }

        public bool CityExists(int id)
        {
            return _context.Cities.Any(e => e.Id == id);
        }
    }
}
