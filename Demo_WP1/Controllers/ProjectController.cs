using Demo_WP1.Models;
using Microsoft.Ajax.Utilities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Demo_WP1.Controllers
{
    public class ProjectController : Controller
    {
        dbProjectDataContext db = new dbProjectDataContext();



        public ActionResult getAllProject(string category, string type, string key, int? size, int? page)
        {
            if (Session["Customer"] == null || Session["Customer"].ToString() == "")
            {
                return RedirectToAction("Login", "User");
            }

            List<project> all_projects = db.projects.ToList();

            if(category != null)
            {
                all_projects = db.projects.Where(p => p.category == category).ToList();
            }

            ViewBag.key = key;
            
            switch (type)
            {
                case "name":
                    {
                        all_projects = all_projects.OrderBy(p => p.name).ToList();
                        break;
                    }
                case "price":
                    {
                        all_projects = all_projects.OrderBy(p => p.price).ToList();
                        break;
                    }
                default:
                    {
                        all_projects = all_projects.OrderBy(p => p.publish_date).ToList();
                        break;
                    }
            }
            ViewBag.type = type;
            ViewBag.currentSize = size;
            if (page == null) page = 1;
            if (!string.IsNullOrEmpty(category))
            {

                if (!string.IsNullOrEmpty(key))
                {
                    all_projects = all_projects.Where(a => (a.name.Contains(key) || a.description.Contains(key))
                                    && a.category.Contains(category)).ToList();

                }
                else
                {
                    all_projects = all_projects.Where(a => a.category.Contains(category)).ToList();
                }

            }
            else if (!string.IsNullOrEmpty(key))
            {
                all_projects = all_projects.Where(a => a.name.Contains(key) || a.description.Contains(key)).ToList();

            }

            int pageSize = (size ?? 6);
            int pageNum = page ?? 1;
            ViewBag.category = category;
            return View(all_projects.ToPagedList(pageNum, pageSize));
        }
        public ActionResult Detail(int id) 
        {
            var project = db.projects.First(p => p.id == id);
            ViewBag.category = project.category;
            return View(project);
        }
    }
}