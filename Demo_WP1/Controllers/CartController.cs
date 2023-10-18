using Demo_WP1.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Demo_WP1.Controllers
{
    public class CartController : Controller
    {
        dbProjectDataContext db = new dbProjectDataContext();
        public List<Cart> getCart()
        {
            List<Cart> listCart = Session["Cart"] as List<Cart>;
            if (listCart == null)
            {
                listCart = new List<Cart>();
                Session["Cart"] = listCart;
            }
            return listCart;
        }
        public ActionResult AddCart(int id, string strURL)
        {
            List<Cart> listCart = getCart();
            Cart cart = listCart.Find(p => p.project_id == id);
            if (cart == null)
            {   
                cart = new Cart(id);
                listCart.Add(cart);
            }
            return Redirect(strURL);
        }
        public int sumProjectQuantity()
        {
            int sum = 0;
            List<Cart> listCart = Session["Cart"] as List<Cart>;
            if (listCart != null)
            {
                sum = listCart.Count;
            }
            return sum;
        }
        public ActionResult Cart()
        {
            List<Cart> listCart = getCart();
            ViewBag.sumProjectQuantity = sumProjectQuantity();
            return View(listCart);
        }
        public ActionResult CartPartial()
        {
            ViewBag.sumProjectQuantity = sumProjectQuantity();
            return PartialView();
        }
        public ActionResult CartDelete(int id)
        {
            List<Cart> listCart = getCart();
            Cart project = listCart.SingleOrDefault(p => p.project_id == id);
            if (project != null)
            {
                listCart.Remove(project);
                return RedirectToAction("Cart");
            }
            return RedirectToAction("Cart");
        }
        public ActionResult AllCartDelete()
        {
            List<Cart> listCart = getCart();
            listCart.Clear();
            return RedirectToAction("Cart");
        }

        [HttpGet]
        public ActionResult PlaceOrder()
        {
            List<Cart> listCart = getCart();
            ViewBag.sumProjectQuantity = sumProjectQuantity();
            return View(listCart);
        }
        public ActionResult PlaceOrder(FormCollection collection)
        {
            buy b = new buy();
            customer c = (customer)Session["Customer"];
            project p = new project();
            List<Cart> listCart = getCart();
            b.customer_id = c.id;
            b.project_id = 1;
            b.date = DateTime.Now;
            db.buys.InsertOnSubmit(b);
            db.SubmitChanges();
            var strProject = "";
            var totalMoney = decimal.Zero;
            foreach (var item in listCart)
            {
                strProject += "<tr>";
                strProject += "<td>" + item.project_id + "</td>";
                strProject += "<td>" + item.project_name + "</td>";
                strProject += "<td>" + item.category + "</td>";
                strProject += "<td>" + item.price + "</td>";
                strProject += "<td>" + item.publish_date + "</td>";
                strProject += "</tr>";
                totalMoney += item.price;
                p = db.projects.Single(n => n.id == item.project_id);
                db.SubmitChanges();
            }
            db.SubmitChanges();
            //Send mail
            string customer = System.IO.File.ReadAllText(Server.MapPath("~/Content/templates/send2.html"));
            customer = customer.Replace("{{MaDon}}", b.id.ToString());
            customer = customer.Replace("{{TenKhachHang}}", c.full_name);
            customer = customer.Replace("{{NgayDatHang}}", b.date.ToString());
            customer = customer.Replace("{{SanPham}}", strProject);
            customer = customer.Replace("{{Phone}}", c.phone);
            customer = customer.Replace("{{DiaChi}}", c.address);
            customer = customer.Replace("{{Email}}", c.email);
            Demo_WP1.Common.Common.SendMail("BuyProject", "Project: " + b.id.ToString(), customer, "loc2462003@gmail.com");
            Session["Cart"] = null;
            return RedirectToAction("ConfirmOrder", "Cart");
        }
        public ActionResult ConfirmOrder()
        {
            return View();
        }
    }
}