using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VTYSProje.Models
{
    
        public class ShoppingDetails
        {

            public string UserName { get; set; }
            //[Required(ErrorMessage = "lütfen Adres basligini giriniz")]
            public string AdresBasligi { get; set; }
           
            public string Adres { get; set; }
            
            public string Sehir { get; set; }
           
            public string Semt { get; set; }
           
            public string Mahalle { get; set; }
            
            public string PostaKodu { get; set; }
        }
    
}