using Microsoft.AspNet.Identity.Owin;
using MovieTickets.Models;
using MovieTickets.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace MovieTickets.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;
        private ApplicationUserManager _userManager;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        public MoviesController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
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

            reservationData.User = UserManager.FindById(reservationData.UserId);

            _ = SendEmailReservationConfirmationAsync(reservationData.User.Id, reservationData.User.UserName, reservationData.Id, "Reservation Confirmation!");

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

        private async Task<string> SendEmailReservationConfirmationAsync(string userID, string userName, int reservationId, string subject)
        {
            var callbackUrl = Url.Action("ReservationDetails", "Profile",
               new { id = reservationId }, protocol: Request.Url.Scheme);

            await UserManager.SendEmailAsync(userID, subject,
               "<h1>Reservation Confirmation!</h1><br>Hello, " + userName + ". You can view your reservation by clicking <a href=\"" + callbackUrl + "\">here</a>");

            return callbackUrl;
        }
    }
}