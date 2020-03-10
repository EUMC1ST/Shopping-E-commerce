using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace checkoutservice.Models
{
    public class ProductInfo
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public Price Price { get; set; }
        public string Category { get; set; }
    }
}
