using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VTYSProje.Entity;
using VTYSProje.Models;

namespace VTYSProje.Controllers
{
    
    [Authorize(Roles = "admin")]
    public class OrderController : Controller
    {
        private VTYSProjeEntities db = new VTYSProjeEntities();
        // GET: Order
        public ActionResult Index()
        {
            var orders = db.Order.Select(i => new AdminOrderModel()
            {
                Id = i.Id,
                OrderNumber = i.OrderNumber,
                OrderState = i.OrderState,
                DateTime = i.DateTime,
                Total = i.Total,
                Count = i.OrderLİne.Count
            }).ToList();

            return View(orders);
        }

        public ActionResult Details(int Id)
        {
            var product = db.Order.Where(i => i.Id == Id)
                .Select(i => new OrderDetailsModel()
                {
                    UserName = i.UserName,
                    OrderId = i.Id,
                    OrderNumber = i.OrderNumber,
                    Total = i.Total,
                    DateTime = i.DateTime,
                    OrderState = i.OrderState,
                    AdresBasligi = i.AdresBasligi,
                    Adres = i.Adres,
                    //Mahalle = i.Mahalle,
                    //Sehir = i.Sehir,
                    //Semt = i.Semt,
                    //PostaKodu = i.PostaKodu,
                    OrderLines = i.OrderLİne.Select(a => new OrderLineModel()
                    {
                        Quantity = a.Quantity,
                        Price = a.Price,
                        Images = a.Product.Image,
                        ProductId = a.ProductId,
                        ProductName = a.Product.Name
                    }).ToList()

                }).FirstOrDefault();

            return View(product);
        }

        public ActionResult UpdateOrderState(int OrderId, int OrderState)
        {
            var order = db.Order.FirstOrDefault(i => i.Id == OrderId);
            if (order != null)
            {

                order.OrderState = OrderState;
                db.SaveChanges();
                TempData["message"] = "Bilgileriniz Kayıt Edildi.";
                return RedirectToAction("Details", new { id = OrderId });
            }

            return View("Index");
        }
    }
}