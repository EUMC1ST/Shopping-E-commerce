using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace checkoutservice.Models
{
    public class UserInfo
    {
        public int UserId { get; set; }
        public string NumTarget { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
    }
}
