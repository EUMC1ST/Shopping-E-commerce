using checkoutservice.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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

        public virtual Cart CartService(int userID)
        {
            pathController = "api/cart?userId=" + userID;
            return request.TheGet<Cart>(pathController, "https://academia-cartservice.azurewebsites.net/");
        }
        public virtual ProductInfo ProductCatalog(int productID)
        {
            pathController = "api/productCatalogService/"+productID;
            return request.TheGet<ProductInfo>(pathController, "https://academiaproductcatalogservice.azurewebsites.net/");
        }

        //INVENTADO--------------------------

        public virtual double Currency(CurrencyChange currencyChange)
        {
            pathController = "api/currency";
            return request.ThePost<CurrencyChange,double>(currencyChange, pathController, "https://academia-currencyservice.azurewebsites.net/");
        }

        public virtual double Shipping(double totalCostOfProducts)
        {
            pathController = ""+totalCostOfProducts;
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
