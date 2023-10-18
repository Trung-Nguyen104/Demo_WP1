using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Demo_WP1.Models
{
    public class Cart
    {
        dbProjectDataContext db = new dbProjectDataContext();
        public int project_id { get; set; }

        [Display(Name = "Project name")]
        public string project_name { get; set; }

        [Display(Name = "Category")]
        public string category { get; set; }

        [Display(Name = "Price")]
        public long price { get; set; }

        [Display(Name = "Publish date")]
        public string publish_date { get; set; }

        public Cart(int id)
        {
            project_id = id;
            project p = db.projects.First(project => project.id == id);
            project_name = p.name;
            category = p.category;
            price = p.price;
            publish_date = p.publish_date.ToString();
        }
    }
}