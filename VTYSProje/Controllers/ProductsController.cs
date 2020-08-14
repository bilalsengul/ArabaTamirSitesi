using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using VTYSProje.Entity;

namespace VTYSProje.Controllers
{
    [Authorize(Roles ="admin")]
    public class ProductsController : Controller
    {
        private VTYSProjeEntities db = new VTYSProjeEntities();
        private Yordam sp = new Yordam();

        // GET: Product
        public ActionResult Index()
        {
            //function ile ürünlerin toplam fiyatlarını aldım.
            var sayi = sp.fonkSum();

            //urunleri listeledim.
            var urunler = db.Product
                 .SqlQuery("Select * from Product")
                 .ToList();
            ViewBag.urumToplam = sayi;
            ViewBag.ID = getir();
            return View(urunler);
        }

        // GET: Product/Create
        public ActionResult Create()
        {

            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Image,Price,Stock,IsHome,IsApproved,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                //web tarafından gelen dataları sp üzerinden database ekledim.               
                sp.addProduct(product);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            //where ile filtre uyguladım.
            var product = db.Product
                 .SqlQuery("Select * from Product where Id=@p0",
                 new SqlParameter("@p0", id)).FirstOrDefault();

            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Image,Price,Stock,IsHome,IsApproved,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                //store procedure ile veritabanındaki bir ürünü güncelleştirdim.
                sp.updateProduct(product);

                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Category, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = db.Product
                 .SqlQuery("Select * from Product where Id=@p0",
                 new SqlParameter("@p0", id)).FirstOrDefault();
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);

        }
        // POST: Comment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comment.Find(id);
            db.Comment.Remove(comment);
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

        public double getir()
        {
            return db.Product.Sum(i => i.Price);
        }
    }
}
