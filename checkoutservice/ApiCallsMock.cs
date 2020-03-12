using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using checkoutservice.Models;
using Microsoft.AspNetCore.Mvc;

namespace checkoutservice
{
    public class ApiCallsMock : ApiCalls
    {
        List<ProductInfo> listProductos = new List<ProductInfo>()
            {
                new ProductInfo(){ ProductID = 1,
                    ProductName ="Tenis",
                    Description = "jndoqn",
                    Picture = "unload.img",
                    Price = new Price(){
                        CurrencyCode = "USD",
                        Nano = 9999,
                        Units = 67},
                    Category ="Vintage"},
                new ProductInfo(){ProductID = 2,
                ProductName = "Chanclas",
                Description="klsandaldmaldk",
                Picture = "unload2.img",
                Price = new Price(){ CurrencyCode="USD",
                Nano = 58999,
                Units = 79},
                Category = "ChanclasVintage"}
            };
        public override Cart CartService(int userID)
        {
            List<Items> productos = new List<Items>() {
                    new Items() { ProductId = 1, Quantity = 2 },
                    new Items() { ProductId = 2, Quantity = 3 }
                };
            Cart carro = new Cart() { UserId = 987, Productos = productos };
            if (carro.UserId == userID)
            {
                return carro;
            }
            return null;
        }

        public override ProductInfo ProductCatalog(int productID)
        {
            var producto = listProductos.Where(x => x.ProductID == productID).First();
            return producto;
        }

        public override double Currency(CurrencyChange currencyChange)
        {
            double pago=0;
            if(currencyChange.CurrencyType == "MXN")
            {
                 pago = currencyChange.Units * 15;
            }
            return pago;
        }

        public override double Shipping(double totalCostOfProducts)
        {
            return  totalCostOfProducts * 0.10;
        }
        public override string Payment(PaymentModel paymentModel)
        {
            return "391030298310983";
        }
        public override ActionResult Email(Order CustomerOrder)
        {
            return new OkResult();
        }
    }
}
