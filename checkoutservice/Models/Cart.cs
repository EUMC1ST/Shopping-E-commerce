using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace checkoutservice.Models
{
    public class Cart
    {
        public int UserId { get; set; }
        public List<Items> Productos { get; set; }
    }
}
