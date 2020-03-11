using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace emailservice.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public string ShippingTrackingId { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public List<Item> Items { get; set; }

    }
}
