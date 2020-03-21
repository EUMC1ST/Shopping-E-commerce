using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace checkoutservice.Models
{
    public class Order
    {
        public string Id { get; set; }
        public Customer Customer { get; set; }
        public string ShippingTrackingId { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public List<Items> Items { get; set; }
    }
}
