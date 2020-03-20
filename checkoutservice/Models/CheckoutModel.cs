using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace checkoutservice.Models
{
    public class CheckoutModel
    {
        public string Message { get; set; }
        public string OderId { get; set; }
        public string ShippingTrackingId { get; set; }
        public double TotalToPay { get; set; }
    }
}
