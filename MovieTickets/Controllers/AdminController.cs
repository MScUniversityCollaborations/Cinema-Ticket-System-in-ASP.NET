using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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

        // Details
        [HttpGet]
        public ActionResult AuditoriumDetails(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var auditorium = _context.Auditoriums.Find(id);

            if (auditorium == null)
                return HttpNotFound();

            return View(auditorium);
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

        public async Task<ActionResult> UserRolesUpdateAsync(string id)
        {
            var user = await UserManager.FindByIdAsync(id);

            var userManager = new UserManager<IdentityUser, string>(new UserStore<IdentityUser>(_context));

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("Error");
            }

            var viewModel = new UserRolesFormViewModel(user)
            {
                Roles = userRoles
            };

            return View("UserForm", viewModel);
        }

        // Role ID is passed from the URL to the action
        /*[HttpGet]
        [Authorize(Roles = RoleName.AdminRole)]
        public async Task<ActionResult> UserRoleUpdate(string id)
        {
            var roleManager = new Microsoft.AspNet.Identity.RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));

            // Find the role by Role ID
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }

            var model = new UserRolesFormViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            // Retrieve all the Users
            foreach (var user in UserManager.Users)
            {
                // If the user is in this role, add the username to
                // Users property of EditRoleViewModel. This model
                // object is then passed to the view for display
                if (await UserManager.IsInRoleAsync(user.Id, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }*/

        // This action responds to HttpPost and receives EditRoleViewModel
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.AdminRole)]
        public async Task<ActionResult> UserRoleUpdate(UserRolesFormViewModel model)
        {
            var roleManager = new Microsoft.AspNet.Identity.RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));

            var role = await roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;

                // Update the Role using UpdateAsync
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }

                return View(model);
            }
        }*/

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
    }
}