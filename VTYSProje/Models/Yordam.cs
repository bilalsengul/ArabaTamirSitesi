using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VTYSProje.Entity;

namespace VTYSProje
{
   
    public class Yordam
    {
        private VTYSProjeEntities db = new VTYSProjeEntities();
        public void addProduct(Product product)
        {
            db.Database.ExecuteSqlCommand("SP_ADDPRODUCT " + "'" + product.Name + "','" + product.Description + "','" + product.Image + "','" + product.Price + "'," +
                     "'" + product.Stock + "','" + product.IsHome + "','" + product.IsApproved + "','" + product.CategoryId + "'");            
        }

        public void updateProduct(Product product)
        {
            db.Database.ExecuteSqlCommand("SP_UPDATEPRODUCT " + "'" + product.Name + "','" + product.Description + "','" + product.Image + "','" + product.Price + "'," +
                     "'" + product.Stock + "','" + product.IsHome + "','" + product.IsApproved + "','" + product.CategoryId + "','" + product.Id +"' ");
        }

        public Double fonkSum() {
            var sayi = db.Database.ExecuteSqlCommand("SELECT DBO.FONK_SUM()");
            return sayi;
        }

        public void addCategory(Category kategori)
        {
            db.Database.ExecuteSqlCommand("SP_ADDCategory " + "'" + kategori.Name + "','" + kategori.Description + "','" + kategori.BrandId + "','" + kategori.Category1Id +"'");
        }


        public void updateCategory(Category kategori)
        {
            db.Database.ExecuteSqlCommand("SP_UPDATECATEGORY " + "'" + kategori.Name + "','" + kategori.Description + "','" + kategori.BrandId + "','" + kategori.Category1Id + "','"+kategori.Id+"'" );
        }

    }
}