using System.Web.Mvc;

namespace MovieTickets.Controllers.Api
{
    public class ScreeningsController : Controller
    {
        // GET: Screenings
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetScreenings()
        {
            /*using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var events = dc.Events.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }*/

            return new JsonResult { Data = "{}", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}