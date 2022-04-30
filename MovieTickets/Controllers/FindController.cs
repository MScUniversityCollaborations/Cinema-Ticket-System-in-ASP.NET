using MovieTickets.Models;
using MovieTickets.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace MovieTickets.Controllers
{
    public class FindController : Controller
    {
        private ApplicationDbContext _context;
        private ApplicationUserManager _userManager;

        public FindController()
        {
            _context = new ApplicationDbContext();
        }

        public FindController(ApplicationUserManager userManager)
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

        [HttpGet]
        public ActionResult FindReservation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchForReservation(FindReservationFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Model is invalid";

                return View("Error");
            }

            var reservationsInDb = _context.Reservations
                    .Include(m => m.Screening)
                    .Include(m => m.Screening.Movie)
                    .Include(m => m.Screening.Auditorium)
                    .Include(m => m.User)
                    .Where(r => r.User.Email == model.Email)
                    .Where(r => r.User.PhoneNumber == model.PhoneNumber)
                    .OrderBy(m => m.Screening.ScreeningStart);

            if (!reservationsInDb.Any())
            {
                ViewBag.ErrorMessage = "No reservations were found!";

                return View("Error");
            }

            var viewModel = new ReservationsListFormViewModel()
            {
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Reservations = reservationsInDb
            };

            return View("ReservationsList", viewModel);
        }

        [HttpGet]
        public ActionResult ConfirmReservation(int id)
        {
            var reservationInDb = _context.Reservations.Single(m => m.Id == id);
            var user = UserManager.FindById(reservationInDb.UserId);

            var viewModel = new ConfirmReservationFormViewModel(reservationInDb, user);

            return View("ConfirmReservation", viewModel);
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public ActionResult ConfirmReservation([Bind(Include = "Id, FirstName, LastName, Email, PhoneNumber")] ConfirmReservationFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Model is invalid";

                return View("Error");
            }

            var reservationInDb = _context.Reservations.Single(m => m.Id == model.Id);

            if (Request.Files.Count >= 1)
            {
                var file = Request.Files[0];
                var imgBytes = new Byte[file.ContentLength - 1];
                file.InputStream.Read(imgBytes, 0, imgBytes.Length);
                var base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);

                reservationInDb.Photo = base64String;
            }

            _context.SaveChanges();

            ViewBag.Message = "Welcome and enjoy your time! The reservation has been confirmed!";

            return View("Info");
        }
    }
}