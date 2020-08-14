using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VTYSProje.Entity;

namespace VTYSProje.Models
{
    public class OrdersController : Controller
    {
        private VTYSProjeEntities db = new VTYSProjeEntities();

        // GET: Orders
        public ActionResult Index()
        {
            var order = db.Order.Include(o => o.City).Include(o => o.Country).Include(o => o.District);
            return View(order.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.CıtyId = new SelectList(db.City, "Id", "Name");
            ViewBag.CountryId = new SelectList(db.Country, "Id", "Name");
            ViewBag.DistrictId = new SelectList(db.District, "Id", "Name");
            return View();
        }

        // POST: Orders/Create
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderNumber,Total,UserName,OrderState,AdresBasligi,Adres,CıtyId,CountryId,DistrictId,DateTime")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Order.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CıtyId = new SelectList(db.City, "Id", "Name", order.CıtyId);
            ViewBag.CountryId = new SelectList(db.Country, "Id", "Name", order.CountryId);
            ViewBag.DistrictId = new SelectList(db.District, "Id", "Name", order.DistrictId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CıtyId = new SelectList(db.City, "Id", "Name", order.CıtyId);
            ViewBag.CountryId = new SelectList(db.Country, "Id", "Name", order.CountryId);
            ViewBag.DistrictId = new SelectList(db.District, "Id", "Name", order.DistrictId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, lütfen bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için https://go.microsoft.com/fwlink/?LinkId=317598 sayfasına bakın.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderNumber,Total,UserName,OrderState,AdresBasligi,Adres,CıtyId,CountryId,DistrictId,DateTime")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CıtyId = new SelectList(db.City, "Id", "Name", order.CıtyId);
            ViewBag.CountryId = new SelectList(db.Country, "Id", "Name", order.CountryId);
            ViewBag.DistrictId = new SelectList(db.District, "Id", "Name", order.DistrictId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Order.Find(id);
            db.Order.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
