using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VTYSProje.Entity;

namespace VTYSProje.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoriesController : Controller
    {
        private Yordam sp = new Yordam();
        private VTYSProjeEntities db = new VTYSProjeEntities();

        // GET: Categories
        public ActionResult Index()
        {
            //kategorileri listeledim.
            var kategoriler = db.Category
                 .SqlQuery("Select * from Category ")
                 .ToList();
            return View(kategoriler);
        }


        // GET: Categories/Create
        public ActionResult Create()
        {
            ViewBag.BrandId = new SelectList(db.Brand, "Id", "Name");
            ViewBag.Category1Id = new SelectList(db.Category1, "Id", "Name");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,BrandId,Category1Id")] Category category)
        {
            if (ModelState.IsValid)
            {
                //sp ile categor ekledim.
                sp.addCategory(category);
                return RedirectToAction("Index");
            }

            ViewBag.BrandId = new SelectList(db.Brand, "Id", "Name", category.BrandId);
            ViewBag.Category1Id = new SelectList(db.Category1, "Id", "Name", category.Category1Id);
            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var kategori = db.Category
                 .SqlQuery("Select * from Category where Id=@p0",
                 new SqlParameter("@p0", id)).FirstOrDefault();
            if (kategori == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(db.Brand, "Id", "Name", kategori.BrandId);
            ViewBag.Category1Id = new SelectList(db.Category1, "Id", "Name", kategori.Category1Id);
            return View(kategori);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,BrandId,Category1Id")] Category category)
        {
            if (ModelState.IsValid)
            {
                sp.updateCategory(category);
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(db.Brand, "Id", "Name", category.BrandId);
            ViewBag.Category1Id = new SelectList(db.Category1, "Id", "Name", category.Category1Id);
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = db.Category
                 .SqlQuery("Select * from Category where Id=@p0",
                 new SqlParameter("@p0", id)).FirstOrDefault();
            if (category == null)
            {
                return HttpNotFound();
            }
            //sql komutlar ile silme işlemi yaptım.
            db.Database.ExecuteSqlCommand("delete from Category where Id=" + id + "");

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
