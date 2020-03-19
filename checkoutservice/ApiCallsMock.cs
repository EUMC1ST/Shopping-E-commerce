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
                new ProductInfo(){ id = "1",
                    name ="Tenis",
                    description = "jndoqn",
                    picture = "unload.img",
                    priceUsd = new Price(){
                        currencyCode = "USD",
                        nanos = 9999,
                        units = 67},
                    categories = new List<string>(){"Vintage" } },
                new ProductInfo(){ id = "2",
                name = "Chanclas",
                description = "klsandaldmaldk",
                picture = "unload2.img",
                priceUsd = new Price(){ currencyCode = "USD",
                nanos = 58999,
                units = 79},
                categories = new List<string>(){ "ChanclasVintage" } }
            };
        public override Cart CartService(string userID)
        {
            List<Items> productos = new List<Items>() {
                    new Items() { idProduct = "1", quantity = 2 },
                    new Items() { idProduct = "2", quantity = 3 }
                };
            Cart carro = new Cart() { Productos = productos };
            return null;
        }

        public override ProductInfo ProductCatalog(string productID)
        {
            var producto = listProductos.Where(x => x.id == productID).First();
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
