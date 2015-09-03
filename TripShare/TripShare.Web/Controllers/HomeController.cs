using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TripShare.Data;
using TripShare.Data.Repositories;
using TripShare.Models;

namespace TripShare.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Trip Share";

            return View();
        }
    }
}
