using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieTickets.Controllers
{
    public class MoviesController : Controller
    {
        public ActionResult NowShowing()
        {
            return View();
        }

        public ActionResult ComingSoon()
        {
            return View();
        }
    }
}