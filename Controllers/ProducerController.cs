using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VTYSProje.Entity;

namespace VTYSProje.Controllers
{
    [Authorize(Roles ="admin")]
    public class ProducerController : Controller
    {
        private VTYSProjeEntities db = new VTYSProjeEntities();

        // GET: Producer
        public ActionResult Index()
        {
            var producer = db.Producer.Include(p => p.Brand).Include(p => p.City).Include(p => p.Country).Include(p => p.District);
            return View(producer.ToList());
        }

        // GET: Producer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producer producer = db.Producer.Find(id);
            if (producer == null)
            {
                return HttpNotFound();
            }
            return View(producer);
        }

        // GET: Producer/Create
        public ActionResult Create()
        {
            ViewBag.BrandId = new SelectList(db.Brand, "Id", "Name");
            ViewBag.CityId = new SelectList(db.City, "Id", "Name");
            ViewBag.CountryId = new SelectList(db.Country, "Id", "Name");
            ViewBag.DistrictId = new SelectList(db.District, "Id", "Name");
            return View();
        }

        // POST: Producer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BrandId,CountryId,CityId,DistrictId,Adress")] Producer producer)
        {
            if (ModelState.IsValid)
            {
                db.Producer.Add(producer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BrandId = new SelectList(db.Brand, "Id", "Name", producer.BrandId);
            ViewBag.CityId = new SelectList(db.City, "Id", "Name", producer.CityId);
            ViewBag.CountryId = new SelectList(db.Country, "Id", "Name", producer.CountryId);
            ViewBag.DistrictId = new SelectList(db.District, "Id", "Name", producer.DistrictId);
            return View(producer);
        }

        // GET: Producer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producer producer = db.Producer.Find(id);
            if (producer == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(db.Brand, "Id", "Name", producer.BrandId);
            ViewBag.CityId = new SelectList(db.City, "Id", "Name", producer.CityId);
            ViewBag.CountryId = new SelectList(db.Country, "Id", "Name", producer.CountryId);
            ViewBag.DistrictId = new SelectList(db.District, "Id", "Name", producer.DistrictId);
            return View(producer);
        }

        // POST: Producer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BrandId,CountryId,CityId,DistrictId,Adress")] Producer producer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(db.Brand, "Id", "Name", producer.BrandId);
            ViewBag.CityId = new SelectList(db.City, "Id", "Name", producer.CityId);
            ViewBag.CountryId = new SelectList(db.Country, "Id", "Name", producer.CountryId);
            ViewBag.DistrictId = new SelectList(db.District, "Id", "Name", producer.DistrictId);
            return View(producer);
        }

        // GET: Producer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producer producer = db.Producer.Find(id);
            if (producer == null)
            {
                return HttpNotFound();
            }
             producer = db.Producer.Find(id);
            db.Producer.Remove(producer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //// POST: Producer/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Producer producer = db.Producer.Find(id);
        //    db.Producer.Remove(producer);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
