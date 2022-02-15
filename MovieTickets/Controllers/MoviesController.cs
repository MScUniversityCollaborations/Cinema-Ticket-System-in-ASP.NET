using MovieTickets.Models;
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

        public ActionResult Details() 
        { 
            return View();
        }

        [Authorize(Roles = RoleName.CanBuyTickets)]
        public ActionResult BuyTicket()
        {
            return View();
        }

        public ActionResult SelectDateTime()
        {
            return View();
        }

        public ActionResult SelectSeat()
        {
            return View();
        }
    }
}