using Demo_WP1.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo_WP1.Controllers
{
    public class ProjectController : Controller
    {
        dbProjectDataContext db = new dbProjectDataContext();
        public ActionResult getProjects(string category)
        {
            Listall_project = db.projects.All(c => c.category == category);
        }
    }
}