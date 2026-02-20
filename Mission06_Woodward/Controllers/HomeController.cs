using Microsoft.AspNetCore.Mvc;
using Mission06_Woodward.Models;

namespace Mission06_Woodward.Controllers
{
    public class HomeController : Controller
    {
        private readonly MovieContext _context;

        public HomeController(MovieContext temp)
        {
            _context = temp;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetToKnowJoel()
        {
            return View();
        }

        // READ
        public IActionResult MovieList()
        {
            var movies = _context.Movies.ToList();
            return View(movies);
        }

        // CREATE (GET)
        [HttpGet]
        public IActionResult MovieForm()
        {
            return View();
        }

        // CREATE + UPDATE (POST)
        [HttpPost]
        public IActionResult MovieForm(Movie response)
        {
            // Automatically assign default category if null
            if (response.CategoryId == null)
            {
                response.CategoryId = 1;
            }

            if (ModelState.IsValid)
            {
                if (response.MovieId == 0)
                {
                    _context.Movies.Add(response);
                }
                else
                {
                    _context.Movies.Update(response);
                }

                _context.SaveChanges();
                return RedirectToAction("MovieList");
            }

            return View(response);
        }

        // EDIT (GET)
        public IActionResult Edit(int id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.MovieId == id);
            return View("MovieForm", movie);
        }

        // DELETE (GET)
        public IActionResult Delete(int id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.MovieId == id);
            return View(movie);
        }

        // DELETE (POST)
        [HttpPost]
        public IActionResult DeleteConfirmed(Movie movie)
        {
            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return RedirectToAction("MovieList");
        }
    }
}