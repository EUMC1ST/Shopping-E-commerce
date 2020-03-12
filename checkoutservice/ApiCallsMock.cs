using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using checkoutservice.Models;

namespace checkoutservice
{
    public class ApiCallsMock : ApiCalls
    {
        public override Cart CartService(int userID)
        {
            List<Items> productos = new List<Items>() {
                    new Items() { ProductId = 1, Quantity = 2 },
                    new Items() { ProductId = 2, Quantity = 3 }
                };
            Cart carro = new Cart() { UserId = 987, Productos = productos };
            return carro;
        }

        public override ProductInfo ProductCatalog(int productID)
        {
            return base.ProductCatalog(productID);
        }
    }
}
