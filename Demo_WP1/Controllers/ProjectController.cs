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
        public ActionResult getAllProject(string category)
        {
            List<project> all_projects = db.projects.Where(p => p.category == category).ToList();
            return View(all_projects);
        }
        public ActionResult Detail(int id) 
        {
            var project = db.projects.First(p => p.id == id);
            ViewBag.category = project.category;
            return View(project);
         }
    }
}