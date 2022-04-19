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
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public AdminController()
        {
            _context = new ApplicationDbContext();
        }

        public AdminController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
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

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
                _context.Dispose();
            }

            base.Dispose(disposing);
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
        public ActionResult Reservations()
        {
            var reservations = _context.Reservations
                .Include(m => m.Screening)
                .Include(m => m.User);

            return View(reservations);
        }

        [Authorize(Roles = RoleName.AdminRole)]
        public ActionResult MovieUpdate(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }


        [Authorize(Roles = RoleName.AdminRole)]
        public ActionResult AuditoriumUpdate(byte id)
        {
            var auditorium = _context.Auditoriums
                                    .SingleOrDefault(c => c.Id == id);

            if (auditorium == null)
                return HttpNotFound();

            var viewModel = new AuditoriumFormViewModel(auditorium);

            return View("AuditoriumsForm", viewModel);
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

        // Edits/Updates
        [Authorize(Roles = RoleName.AdminRole)]
        public async Task<ActionResult> UserUpdate(string id)
        {

            var user = await UserManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("Error");
            }

            // GetClaimsAsync returns the list of user Claims
            var userClaims = await UserManager.GetClaimsAsync(user.Id);
            // GetRolesAsync returns the list of user Roles
            var userRoles = await UserManager.GetRolesAsync(user.Id);

            var viewModel = new UserFormViewModel(user)
            {
                Claims = userClaims.Select(c => c.Value).ToList(),
                Roles = userRoles
            };

            return View("UserForm", viewModel);
        }

        [HttpGet]
        [Authorize(Roles = RoleName.AdminRole)]
        public async Task<ActionResult> ManageUserRoles(string Id)
        {
            ViewBag.userId = Id;

            var user = await UserManager.FindByIdAsync(Id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {Id} cannot be found";
                return View("Error");
            }

            var model = new List<UserRolesFormViewModel>();

            foreach (var role in RoleManager.Roles.ToList())
            {
                var userRolesViewModel = new UserRolesFormViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if (await UserManager.IsInRoleAsync(user.Id, role.Name))
                {
                    userRolesViewModel.IsSelected = true;
                }
                else
                {
                    userRolesViewModel.IsSelected = false;
                }

                model.Add(userRolesViewModel);
            }

            return View("UserRolesForm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.AdminRole)]
        public ActionResult ManageUserRoles(List<UserRolesFormViewModel> model, string userId)
        {
            var user = UserManager.FindById(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = '{userId}' cannot be found";
                return View("Error");
            }

            var roles = UserManager.GetRoles(user.Id);
            /*var result = await UserManager.RemoveFromRolesAsync(user.Id, roles.ToArray());

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("Error", m);
            }*/

            if (ModelState.IsValid)
            {
                foreach (var role in model.Where(m => m.IsSelected))
                {
                    var result = UserManager.AddToRoles(user.Id,
                                role.RoleName);

                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", "Cannot add selected roles to user");
                        ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                        return View("Error", model);
                    }
                }

                ViewBag.Message = "User roles have been updated successfully!";

                return View("Info");
            } 
            else
            {
                ViewBag.ErrorMessage = "Model is invalid";

                return View("Error");
            }

            //return RedirectToAction("UserUpdate", new { Id = userId });
        }

        [Authorize(Roles = RoleName.AdminRole)]
        public ViewResult AddUser()
        {

            var viewModel = new UserFormViewModel();

            return View("UserForm", viewModel);
        }

        [HttpGet]
        [Authorize(Roles = RoleName.AdminRole)]
        public ViewResult AddAuditorium()
        {
            var viewModel = new AuditoriumFormViewModel();

            return View("AuditoriumsForm", viewModel);
        }

        [Authorize(Roles = RoleName.AdminRole)]
        public ViewResult AddMovie()
        {
            var genres = _context.Genres.ToList();

            var viewModel = new MovieFormViewModel
            {
                Genres = genres
            };

            return View("MovieForm", viewModel);
        }

        [HttpGet]
        [Authorize(Roles = RoleName.AdminRole)]
        public ViewResult AddScreening()
        {
            var viewModel = new ScreeningFormViewModel
            {
                Movies = _context.Movies.ToList(),
                Auditoriums = _context.Auditoriums.ToList()
            };

            return View("ScreeningForm", viewModel);
        }

        [HttpGet]
        [Authorize(Roles = RoleName.AdminRole)]
        public ActionResult UserDetails(string id)
        {
            ApplicationUser user = UserManager.FindById(id);

            if (user == null)
                return HttpNotFound();

            return View(user);
        }

        [HttpGet]
        [Authorize(Roles = RoleName.AdminRole)]
        public ActionResult AuditoriumDetails(byte id)
        {
            var auditorium = _context.Auditoriums
                .SingleOrDefault(m => m.Id == id);

            if (auditorium == null)
                return HttpNotFound();

            return View(auditorium);
        }

        [HttpGet]
        [Authorize(Roles = RoleName.AdminRole)]
        public ActionResult ScreeningDetails(int id)
        {
            var screening = _context.Screenings
                .SingleOrDefault(m => m.Id == id);

            if (screening == null)
                return HttpNotFound();

            return View(screening);
        }

        // GET: Auditoriums/Delete/5
        [HttpGet]
        [Authorize(Roles = RoleName.AdminRole)]
        public ActionResult AuditoriumDelete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Auditorium auditorium = _context.Auditoriums
                                            .SingleOrDefault(m => m.Id == id);
            if (auditorium == null)
            {
                return HttpNotFound();
            }

            return View("AuditoriumDelete", auditorium);
        }

        // POST: Auditoriums/Delete/5
        [HttpPost, ActionName("AuditoriumDelete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.AdminRole)]
        public ActionResult AuditoriumDelete(byte id)
        {
            Auditorium auditorium = _context.Auditoriums
                                            .SingleOrDefault(m => m.Id == id);

            _context.Auditoriums.Remove(auditorium);
            _context.SaveChanges();

            ViewBag.Message = "Auditorium has been removed successfully!";

            return View("Info");
        }

        // GET: Screenings/Delete/5
        [HttpGet]
        [Authorize(Roles = RoleName.AdminRole)]
        public ActionResult ScreeningDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Screening screening = _context.Screenings
                                          .SingleOrDefault(m => m.Id == id);
            if (screening == null)
            {
                return HttpNotFound();
            }

            return View("ScreeningDelete", screening);
        }

        // POST: Screenings/Delete/5
        [HttpPost, ActionName("ScreeningDelete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.AdminRole)]
        public ActionResult ScreeningDelete(int id)
        {
            Screening screening = _context.Screenings
                                           .SingleOrDefault(m => m.Id == id);

            _context.Screenings.Remove(screening);
            _context.SaveChanges();

            ViewBag.Message = "Screening has been removed successfully!";

            return View("Info");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.AdminRole)]
        public ActionResult SaveAuditorium(Auditorium auditorium)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new AuditoriumFormViewModel(auditorium);

                return View("AuditoriumForm", viewModel);
            }

            if (auditorium.Id.Equals(null))
            {
                // Add the auditorium
                _context.Auditoriums.Add(auditorium);
            }
            else
            {
                var auditoriumInDb = _context.Auditoriums.Single(m => m.Id == auditorium.Id);

                auditoriumInDb.Name = auditorium.Name;
                auditoriumInDb.TotalSeats = auditorium.TotalSeats;
            }

            _context.SaveChanges();

            ViewBag.Message = "Auditorium has been added successfully!";

            return View("Info");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.AdminRole)]
        public ActionResult SaveScreening(Screening screening)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new ScreeningFormViewModel(screening)
                {
                    Movies = _context.Movies.ToList(),
                    Auditoriums = _context.Auditoriums.ToList()
                };

                return View("ScreeningForm", viewModel);
            }

            if (screening.Id == 0)
            {
                // Add the screening
                _context.Screenings.Add(screening);
            }
            else
            {
                var screeningInDb = _context.Screenings.Single(m => m.Id == screening.Id);

                screeningInDb.MovieId = screening.MovieId;
                screeningInDb.AuditoriumId = screening.AuditoriumId;
                screeningInDb.ScreeningStart = screening.ScreeningStart;
                screeningInDb.ScreeningEnd = screening.ScreeningEnd;
            }

            _context.SaveChanges();

            ViewBag.Message = "Screening has been added successfully!";

            return View("Info");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.AdminRole)]
        public ActionResult SaveMovie(Movie movie)
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

            ViewBag.Message = "Movie has been added successfully!";

            return View("Info");

            // return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.AdminRole)]
        public ActionResult SaveUser(UserFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = UserManager.FindById(model.Id);

                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;

                UserManager.Update(user);

                ViewBag.Message = "User {" + model.Id + "} has been updated successfully!";

                return View("Info");
            }
            else
            {
                ViewBag.ErrorMessage = "Model is invalid";

                return View("Error");
            }

            // return RedirectToAction("Index", "Admin");

        }
    }
}