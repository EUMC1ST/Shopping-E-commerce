using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace checkoutservice.Models
{
    public class PaymentModel
    {
        public string TargetNumber { get; set; }
        public double TotalToPay { get; set; }
    }
}
