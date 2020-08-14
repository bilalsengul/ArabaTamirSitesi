using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VTYSProje.Entity;
using VTYSProje.Models;

namespace VTYSProje.Controllers
{
    public class HomeController : Controller
    {
        private VTYSProjeEntities db = new VTYSProjeEntities();
        // GET: Home
        public ActionResult Index()
        {
            var urunler = db.Product.Where(i => i.IsHome==1 && i.IsApproved==1).Select(i => new ProductModel()
            {
                Id = i.Id,
                Image = i.Image,
                Name = i.Name,
                Stock = i.Stock,
                Description = i.Description.Substring(0, 80) + "...",            
                Price = i.Price,
                CategoryId = i.CategoryId
            }).ToList();

            return View(urunler);
        }

        public ActionResult Details(int id)
        {
            var product = db.Product.Where(i => i.Id == id).FirstOrDefault();
            ViewBag.Comments = db.Comment.Where(i => i.ProductId == id).ToList();
            return View(product);
        }

        public ActionResult List(int? id)
        {
            var urunler = db.Product.Where(a => a.IsApproved==1).Select(a => new ProductModel()
            {
                Id = a.Id,
                Image = a.Image,
                Name = a.Name,
                Stock = a.Stock,
                Description = a.Description.Substring(0, 80) + "...",
                Price = a.Price,
                CategoryId = a.CategoryId
            }).AsQueryable();
            if (id == null)
            {
                return View(urunler.ToList());
            }
            else
            {
                urunler = urunler.Where(i => i.CategoryId == id);
                return View(urunler.ToList());
            }
        }

        [ChildActionOnly]
        public PartialViewResult _GetCategories()
        {
            var kategoriler = db.Category.Select(i => new CategoryModel()
            {
                Name = i.Name,
                CategoryCaunt = i.Product.Count(),
                Id = i.Id
            })
                .ToList();
            return PartialView("_GetCategories", kategoriler);
        }
    }
}