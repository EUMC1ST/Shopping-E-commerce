using checkoutservice.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace checkoutservice
{
    public class ApiCalls
    {
        HttpRequests request;
        string pathController;

        public ApiCalls()
        {
            request = new HttpRequests();
        }

        public virtual Cart CartService(string userID)
        {
            pathController = "api/CartService/" + userID;
            List<Items> items = request.TheGet<List<Items>>(pathController, "https://academia-cartservice.azurewebsites.net/");
            return new Cart(items);
        }

        public virtual ProductInfo ProductCatalog(string productID)
        {
            pathController = "api/ProductCatalogService/" + productID;
            return request.TheGet<ProductInfo>(pathController, "https://academiaproductcatalogservice.azurewebsites.net/");
        }

        //INVENTADO--------------------------

        public virtual double Currency(CurrencyChange currencyChange)
        {
            pathController = "api/currency/conversion";
            return request.ThePost<CurrencyChange,double>(currencyChange, pathController, "https://academia-currencyservice.azurewebsites.net/");
        }

        public virtual double Shipping(double totalCostOfProducts)
        {
            pathController = "api/shipping" + totalCostOfProducts;
            return request.TheGet<double>(pathController, "https://academia-shippingservice.azurewebsites.net/");
        }

        public virtual string Payment(PaymentModel paymentModel)
        {
            pathController = "api/payment";
            return request.ThePost<PaymentModel,string>(paymentModel, pathController, "https://academia-paymentservice.azurewebsites.net/");
        }

        public virtual ActionResult Email(Order CustomerOrder)
        {
            pathController = "api/Email";
            return request.ThePost<Order, ActionResult>(CustomerOrder, pathController, "https://academy-emailservice.azurewebsites.net/");
        }
    }
}
