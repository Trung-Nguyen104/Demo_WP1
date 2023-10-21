using Demo_WP1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Demo_WP1.Controllers
{
    public class HomeController : Controller
    {
        dbProjectDataContext db = new dbProjectDataContext();
        public ActionResult Home()
        {
            Session["PageTitle"] = "Home";
            return View();
        }
        public ActionResult Category()
        {
            Session["PageTitle"] = "Category";
            return View();
        }
        public ActionResult Contacts() {
            Session["PageTitle"] = "Contacts";
            return View();
        }
    }
}