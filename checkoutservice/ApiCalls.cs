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
            return request.TheGet<Cart>(pathController);
        }
        public virtual ProductInfo ProductCatalog(int productID)
        {
            pathController = "api/productCatalogService/"+productID;
            return request.TheGet<ProductInfo>(pathController);
        }

        //INVENTADO--------------------------

        public virtual double Currency(CurrencyChange currencyChange)
        {
            pathController = "";
            return request.ThePost<CurrencyChange,double>(currencyChange, pathController);
        }

        public virtual double Shipping(double totalCostOfProducts)
        {
            pathController = ""+totalCostOfProducts;
            return request.TheGet<double>(pathController);
        }

        public virtual string Payment(PaymentModel paymentModel)
        {
            pathController = "";
            return request.ThePost<PaymentModel,string>(paymentModel,pathController);
        }

        public virtual ActionResult Email(Order CustomerOrder)
        {
            pathController = "";
            return request.ThePost<Order, ActionResult>(CustomerOrder, pathController);
        }
    }
}
