using Demo_WP1.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Demo_WP1.Controllers
{
    public class UserController : Controller
    {
        dbProjectDataContext db = new dbProjectDataContext();
        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(FormCollection collection, customer c)
        {
            var name = collection["full_name"];
            var password = collection["password"];
            var confirmpassword = collection["confirmpassword"];
            var email = collection["email"];
            var phone = collection["phone"];
            var address = collection["address"];
            if (String.IsNullOrEmpty(confirmpassword))
            {
                ViewData["enterpassword"] = "Please enter password to confirm";
            }
            else
            {
                if (!password.Equals(confirmpassword))
                {
                    ViewData["samepassword"] = "Password not same";
                }
                else
                {
                    c.full_name = name;
                    c.password = password;
                    c.email = email;
                    c.phone = phone;
                    db.customers.InsertOnSubmit(c);
                    db.SubmitChanges();
                    Session["email"] = email;
                    Session["password"] = password;
                    if (Session["email"] != null && Session["password"] != null)
                    {
                        var email_after_register = Session["email"];
                        var password_after_register = Session["password"];
                        customer c_after_register = db.customers.SingleOrDefault(n => n.email == email as string && n.password == password as string);
                        Session["Customer"] = c_after_register;
                    }
                    return RedirectToAction("Home", "Home");
                }
            }
            return this.Register();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var email = collection["email"];
            var password = collection["password"];
            customer c = db.customers.SingleOrDefault(n => n.email == email && n.password == password);
            if (c != null)
            {
                ViewBag.ThongBao = "Login Successful";
                Session["Customer"] = c;
                return RedirectToAction("Home", "Home");
            }
            else
            {
                ViewBag.ThongBao = "Email or password is incorrect";
            }
            return this.Login();
        }

        public ActionResult Account_Info(int id)
        {
            var customer = db.customers.First(c => c.id == id);
            return View(customer);
        }
        [HttpGet]
        public ActionResult product_history(int id)
        {
            List<CustomerHistory> htr1 = db.buys.Join
                (
                    db.projects, t1 => t1.project_id, t2 => t2.id, (t1, t2) =>
                    new CustomerHistory
                    {
                        id = t1.customer_id,
                        cate = t2.category,
                        product_name = t2.name,
                        price = t2.price,
                        order_date = t1.date,
                        image = t2.image,
                        status = t1.status
                    }).Where(p => p.id == id).ToList();
            return View(htr1);
        }
    }

}