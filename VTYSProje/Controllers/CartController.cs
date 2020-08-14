using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VTYSProje.Entity;
using VTYSProje.Models;

namespace VTYSProje.Controllers
{
    public class CartController : Controller
    {
        private VTYSProjeEntities db = new VTYSProjeEntities();
        // GET: Cart
        public ActionResult Index()
        {
            return View(GetCart());
        }

        public ActionResult AddToCart(int Id)
        {
            var product = db.Product.Where(i => i.Id == Id).FirstOrDefault();
            if (product != null)
            {
                GetCart().AddProduckt(product, 1);
            }
            return RedirectToAction("Index");
        }
        public ActionResult clear() {
            GetCart().Clear();
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int Id)
        {
            var product = db.Product.Where(i => i.Id == Id).FirstOrDefault();
            if (product != null)
            {
                GetCart().DeleteProduckt(product);
            }
            return RedirectToAction("Index");
        }
        public Cart GetCart()
        {
            var cart = (Cart)Session["cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["cart"] = cart;
            }

            return cart;
        }

        [ChildActionOnly]
        public PartialViewResult Summary()
        {
            return PartialView("_Summary", GetCart());
        }

        public ActionResult Checkout()
        {
            ViewBag.CıtyId = new SelectList(db.City, "Id", "Name");
            ViewBag.CountryId = new SelectList(db.Country, "Id", "Name");
            ViewBag.DistrictId = new SelectList(db.District, "Id", "Name");
            if (!Request.IsAuthenticated)
            {
                return View("Eror", new string[] { "Lütfen alışverişi tamamlamak icin giriş yapınız" });
            }
            return View(new Order());
        }
        [HttpPost]
        public ActionResult Checkout(Order entity)
        {
            var cart = GetCart();
            if (cart.CartLines.Count == 0)
            {
                ModelState.AddModelError("", "sepette ürün yok");
            }

            if (ModelState.IsValid)
            {
                //siparişi veritabanına kaydet
                //ve sepeti sil
                SaveOrder(cart, entity);
                cart.Clear();
                return View("Complated");
            }
            ViewBag.CıtyId = new SelectList(db.City, "Id", "Name", entity.CıtyId);
            ViewBag.CountryId = new SelectList(db.Country, "Id", "Name", entity.CountryId);
            ViewBag.DistrictId = new SelectList(db.District, "Id", "Name", entity.DistrictId);
            return View(entity);
        }

        
        private void SaveOrder(Cart cart, Order entity)
        {
            
            entity.OrderNumber = "A" + (new Random().Next(11111, 99999)).ToString();
            entity.Total = cart.Total();
            entity.DateTime = DateTime.Now;           
            entity.OrderState =0;
            //entity.OrderLİne = new List<OrderLİne>();
            foreach (var pr in cart.CartLines)
            {
                var orderline = new OrderLİne();
                orderline.Quantity = pr.Quantity;
                orderline.Price = pr.Quantity * pr.Product.Price;
                orderline.ProductId = pr.Product.Id;
                orderline.OrderId = 1;
                db.OrderLİne.Add(orderline);
                
            }

            db.Order.Add(entity);
            db.SaveChanges();
        }
    }
}