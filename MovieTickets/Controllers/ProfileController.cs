using MovieTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult Reservations(string id)
        {
            var reservations = _context.Reservations
                    .Where(r => r.UserId == id)
                    .OrderBy(m => m.Screening.ScreeningStart);

            return View("UserReservations", reservations);
        }

        // GET: Reservations/:id
        [HttpGet]
        [Authorize(Roles = RoleName.AdminRole)]
        public ActionResult ReservationDetails(int id)
        {
            var reservation = _context.Reservations
                .SingleOrDefault(m => m.Id == id);

            if (reservation == null)
                return HttpNotFound();

            return View(reservation);
        }
    }
}