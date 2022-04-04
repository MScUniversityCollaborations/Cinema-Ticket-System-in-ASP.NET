using MovieTickets.Models;
using MovieTickets.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace MovieTickets.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult NowShowing()
        {
            var moviesQuery = _context.Movies
                .Include(m => m.Genre)
                .Where(m => m.NowShowing == true);

            if (moviesQuery == null)
                return HttpNotFound();

            return View(moviesQuery);
        }

        public ActionResult ComingSoon()
        {
            var moviesQuery = _context.Movies
               .Include(m => m.Genre)
               .Where(m => m.NowShowing == false);

            if (moviesQuery == null)
                return HttpNotFound();

            return View(moviesQuery);
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies
                .Include(m => m.Genre)
                .SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        public ActionResult BuyTicket()
        {
            return View();
        }

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.AdminRole)]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };

                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
                movieInDb.Title = movie.Title;
                movieInDb.GenreId = movie.GenreId;

            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }*/

        public ActionResult SelectDateTime()
        {
            return View();
        }

        public ActionResult SelectSeat()
        {
            return View();
        }

        public ActionResult SearchBooking()
        {
            return View();
        }
    }
}