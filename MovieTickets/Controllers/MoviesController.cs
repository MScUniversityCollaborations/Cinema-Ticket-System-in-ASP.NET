using MovieTickets.Models;
using MovieTickets.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
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

        [Authorize]
        public ActionResult SelectDateTime(int id)
        {
            var movieInDb = _context.Movies.Single(m => m.Id == id);
            var screeningsForMovieInDb = _context.Screenings.Where(m => m.MovieId == id).ToList();

            var viewModel = new SelectDateTimeViewModel()
            {
                MovieId = id,
                Movie = movieInDb,
                Screenings = screeningsForMovieInDb
            };

            return View("SelectDateTime", viewModel);
        }

        [Authorize]
        public ActionResult SelectSeat(int id)
        {
            var screeningInDb = _context.Screenings
                .Include(m => m.Movie)
                .SingleOrDefault(m => m.Id == id);

            var reservationsForScreeningInDb = _context.Reservations
                .Include(m => m.Screening)
                .Where(m => m.ScreeningId == id);

            var viewModel = new SelectSeatViewModel()
            {
                ScreeningId = id,
                Screening = screeningInDb,
                Reservations = reservationsForScreeningInDb
            };

            return View("SelectSeat", viewModel);
        }

        [Authorize]
        public ActionResult SearchBooking()
        {
            return View();
        }

        public JsonResult GetScreenings(int id)
        {

            var screeningsForMovieInDb = _context.Screenings
                .Where(m => m.MovieId == id)
                .Include(c => c.Auditorium)
                .Include(c => c.Movie).ToList();

            return new JsonResult { Data = screeningsForMovieInDb, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult CreateReservation(Reservation reservationData)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Model is invalid";

                return View("Error");
            }

            _context.Reservations.Add(reservationData);
            _context.SaveChanges();

            ViewBag.Message = "Reservation has been created successfully!";

            return View("Info");
        }

        [HttpGet]
        [Authorize]
        public ActionResult ReservationCreated()
        {
            ViewBag.Message = "Reservation has been created successfully!";

            return View("Info");
        }
    }
}