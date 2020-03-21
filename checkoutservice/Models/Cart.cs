using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace checkoutservice.Models
{
    public class Cart
    {
        public Cart() { }

        public Cart(List<Items> lista)
        {
            Productos = lista;
        }

        public List<Items> Productos { get; set; }
    }
}
