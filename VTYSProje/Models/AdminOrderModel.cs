using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VTYSProje.Models
{
    public class AdminOrderModel
    {
        
        public int Id { get; set; }
        public int Count { get; set; }
        public string OrderNumber { get; set; }
        public double Total { get; set; }
        public string UserName { get; set; }
        public DateTime DateTime { get; set; }
        public Nullable<int> OrderState { get; set; }
    }
}