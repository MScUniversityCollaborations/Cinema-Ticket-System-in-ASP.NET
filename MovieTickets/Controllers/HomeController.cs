using System.Web.Mvc;

namespace MovieTickets.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}