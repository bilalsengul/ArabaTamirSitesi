using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VTYSProje.Models
{
    public class ProductModel
    {
        [Required]
        [DisplayName("Müşteri Numarası")]
        public int Id { get; set; }

        [Required]
        [DisplayName("Müşteri Adı")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Tamir Açıklama")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Araç Resmi")]
        public string Image { get; set; }

        [Required]
        [DisplayName("Tamir Ücreti")]
        public double Price { get; set; }

        [Required]
        [DisplayName("Araç Sorun Sayısı")]
        public int Stock { get; set; }

        [Required]
        [DisplayName("Anaysafa Gösterim")]
        public int IsHome { get; set; }

        [Required]
        [DisplayName("Tamir Durumu")]
        public int IsApproved { get; set; }

        [Required]
        [DisplayName("Seri Numarası")]
        public int CategoryId { get; set; }
    }
}