using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Demo_WP1.Models
{
    public class Project
    {   
        dbProjectDataContext db = new dbProjectDataContext();
        public int project_id { get; set; }

        [Display(Name = "project name")]
        public string project_name { get; set; }

        [Display(Name = "image")]
        public string project_image { get; set; }

        [Display(Name = "description")]
        public string project_description { get; set; }

        [Display(Name = "category")]
        public string project_category { get; set; }

        [Display(Name = "price")]
        public long project_price { get; set; }

        [Display(Name = "publish_date")]
        public string project_publish_date { get; set; }

        public Project(int id)
        {
            project_id = id;
            project p = db.projects.Single(n => n.id == id);
            project_name = p.name;
            project_image = p.image;
            project_description = p.description;
            project_category = p.category;
            project_price = p.price;
            project_publish_date = p.publish_date.ToString();
        }
    }
}