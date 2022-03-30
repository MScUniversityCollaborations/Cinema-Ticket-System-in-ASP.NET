using System.Web.Mvc;

namespace MovieTickets.Controllers
{
    public class ErrorsController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View("404");
        }
    }
}