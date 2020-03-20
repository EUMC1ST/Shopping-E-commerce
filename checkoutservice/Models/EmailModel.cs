using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace checkoutservice.Models
{
    public class EmailModel
    {
        public string Message { get; set; }
        public int Status { get; set; }
        public string UserPass { get; set; }
    }
}
