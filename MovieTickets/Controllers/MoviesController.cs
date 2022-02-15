using MovieTickets.Models;
using MovieTickets.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            return View();
        }

        public ActionResult ComingSoon()
        {
            return View();
        }

        public ActionResult Details() 
        { 
            return View();
        }

        [Authorize(Roles = RoleName.CanBuyTickets)]
        public ActionResult BuyTicket()
        {
            return View();
        }

        [HttpPost]
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
                if (Request.Files.Count >= 1)
                {
                    var file = Request.Files[0];
                    var imgBytes = new Byte[file.ContentLength - 1];
                    file.InputStream.Read(imgBytes, 0, imgBytes.Length);
                    var base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);

                    movie.ImagePoster = base64String;
                }

                // Add the movies
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);

                movieInDb.Title = movie.Title;
                movieInDb.Director = movie.Director;
                movieInDb.Cast = movie.Cast;
                movieInDb.Description = movie.Description;
                movieInDb.DurationMin = movie.DurationMin;
                movieInDb.GenreId = movie.GenreId;

                if (Request.Files.Count >= 1)
                {
                    var file = Request.Files[0];
                    var imgBytes = new Byte[file.ContentLength - 1];
                    file.InputStream.Read(imgBytes, 0, imgBytes.Length);
                    var base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);

                    movieInDb.ImagePoster = base64String;
                }
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Admin");

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
    }
}