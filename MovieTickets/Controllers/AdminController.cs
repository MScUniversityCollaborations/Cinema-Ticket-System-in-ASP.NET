using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieTickets.Models;
using System.Data.Entity;

namespace MovieTickets.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;

        public AdminController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        [Authorize(Roles = RoleName.AdminRole)]
        public ActionResult Users()
        {
            var users = _context.Users;

            return View(users);
        }

        [HttpGet]
        [Authorize(Roles = RoleName.AdminRole)]
        public ActionResult Movies()
        {
            var movies = _context.Movies
                .Include(m => m.Genre);

            return View(movies);
        }

        [HttpGet]
        [Authorize(Roles = RoleName.AdminRole)]
        public ActionResult Screenings()
        {
            var screenings = _context.Screenings
                .Include(m => m.Auditorium)
                .Include(m => m.Movie);

            return View(screenings);
        }

        [HttpGet]
        [Authorize(Roles = RoleName.AdminRole)]
        public ActionResult Auditoriums()
        {
            var auditoriums = _context.Auditoriums;

            return View(auditoriums);
        }
    }
}