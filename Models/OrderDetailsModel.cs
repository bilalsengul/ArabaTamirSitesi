using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTYSProje.Models
{
    public class OrderDetailsModel
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public double Total { get; set; }

        public string UserName { get; set; }
        public Nullable<int> OrderState { get; set; }
        public string AdresBasligi { get; set; }

        public string Adres { get; set; }

        public string Sehir { get; set; }

        public string Semt { get; set; }

        public string PostaKodu { get; set; }
        public DateTime DateTime { get; set; }
        public virtual List<OrderLineModel> OrderLines { get; set; }
    }

    public class OrderLineModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Images { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

    }
}