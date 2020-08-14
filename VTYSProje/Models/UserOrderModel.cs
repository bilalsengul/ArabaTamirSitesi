using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VTYSProje.Models
{
    public class UserOrderModel
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public double Total { get; set; }
        public string UserName { get; set; }
        public DateTime DateTime { get; set; }
        // public int OrderState { get; set; }
        public Nullable<int> OrderState { get; set; }
    }
}