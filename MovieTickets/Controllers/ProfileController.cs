using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using MovieTickets.Models;
using MovieTickets.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MovieTickets.Controllers
{
    public class ProfileController : Controller
    {
        private ApplicationDbContext _context;

        public ProfileController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Profile/Reservations/:id
        [HttpGet]
        [Authorize]
        public ActionResult Reservations()
        {
            var userId = User.Identity.GetUserId();

            var reservations = _context.Reservations
                    .Include(m => m.Screening)
                    .Include(m => m.Screening.Movie)
                    .Include(m => m.Screening.Auditorium)
                    .Include(m => m.User)
                    .Where(r => r.UserId == userId)
                    .OrderBy(m => m.Screening.ScreeningStart);

            return View("UserReservations", reservations);
        }

        // GET: Reservations/:id
        [HttpGet]
        [Authorize]
        public ActionResult ReservationDetails(int id)
        {
            var reservation = _context.Reservations
                .Include(m => m.Screening)
                .Include(m => m.Screening.Movie)
                .Include(m => m.Screening.Auditorium)
                .Include(m => m.User)
                .SingleOrDefault(m => m.Id == id);

            if (reservation == null)
                return HttpNotFound();

            return View(reservation);
        }
    }
}