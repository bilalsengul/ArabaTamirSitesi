using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using VTYSProje.Entity;

namespace VTYSProje.Controllers
{
   

    [Authorize(Roles = "admin")]
    public class CommentController : Controller
    {
        private VTYSProjeEntities db = new VTYSProjeEntities();

        // GET: Comment
        public ActionResult Index(int? id)
        {

            if (id != null)
            {
                var comments = db.Comment.Where(i => i.ProductId == id);
                return View(comments.ToList());
            }
            else
            {
                var comments = db.Comment.Include(c => c.Product);
                return View(comments.ToList());
            }


        }
        [AllowAnonymous]
        // GET: Comment/Create
        public ActionResult Create(int id)
        {
            ViewBag.ProductId = new SelectList(db.Product, "Id", "Name");
            return View();
        }

        // POST: Comment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Surname,Description,ProductId")] Comment comment)
        {

            if (ModelState.IsValid)
            {
                db.Comment.Add(comment);
                db.SaveChanges();


               



                return RedirectToAction("Details", "", new { id = comment.ProductId });
            }

            ViewBag.ProductId = new SelectList(db.Product, "Id", "Name", comment.ProductId);
            return View(comment);
        }



        // GET: Comment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comment.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
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
    }
}
