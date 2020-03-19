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
        string pathController;

        public virtual Cart CartService(string userID)
        {
            pathController = "api/CartService/" + userID;
            List<Items> items = new HttpRequests().TheGet<List<Items>>(pathController, Environment.GetEnvironmentVariable("CartUrl"));
            return new Cart(items);
        }

        public virtual ProductInfo ProductCatalog(string productID)
        {
            pathController = "api/ProductCatalogService/" + productID;
            return new HttpRequests().TheGet<ProductInfo>(pathController, Environment.GetEnvironmentVariable("ProductCatalogUrl"));
        }

        public virtual double Currency(CurrencyChange currencyChange)
        {
            pathController = "api/currency/conversion";
            return new HttpRequests().ThePost<CurrencyChange,double>(currencyChange, pathController, Environment.GetEnvironmentVariable("CurrencyUrl"));
        }

        public virtual ShippingCost Shipping(ShippingCost totalCostOfProducts)
        {
            pathController = "api/shipping/estimate/"+totalCostOfProducts.calculatedShippingCost;
            return new HttpRequests().ThePost<ShippingCost, ShippingCost>(totalCostOfProducts,pathController, Environment.GetEnvironmentVariable("ShippingUrl"));
        }

        public virtual ShippingTrackingID ShippingTracking(ShippingAddress Address)
        {
            pathController = "api/shipping/tracking";
            return new HttpRequests().ThePost<ShippingAddress, ShippingTrackingID>(Address, pathController, Environment.GetEnvironmentVariable("ShippingUrl"));
        }

        public virtual string Payment(PaymentModel paymentModel)
        {
            pathController = "api/payment";
            return new HttpRequests().ThePost<PaymentModel>(paymentModel, pathController, Environment.GetEnvironmentVariable("PaymentUrl"));
        }

        public virtual ActionResult Email(Order CustomerOrder)
        {
            pathController = "api/Email";
            return new HttpRequests().ThePost<Order, ActionResult>(CustomerOrder, pathController, Environment.GetEnvironmentVariable("EmailUrl"));
        }
    }
}
