using checkoutservice.Models;
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
            //client.BaseAddress = new Uri(BaseUrl);
            //var response = client.GetAsync(pathController+userID);
            //response.Wait();
            //var result = response.Result;
            //var readresult = result.Content.ReadAsStringAsync().Result;
            //var resultadoFinal = JsonConvert.DeserializeObject<Cart>(readresult);
            //return resultadoFinal;
            return request.TheGet<Cart>(pathController);
        }
        public virtual ProductInfo ProductCatalog(int productID)
        {
            //pathController = "api/productCatalogService/";
            //client.BaseAddress = new Uri(BaseUrl);
            //var response = client.GetAsync(pathController + productID);
            //response.Wait();
            //var result = response.Result;
            //var readresult = result.Content.ReadAsStringAsync().Result;
            //var resultadoFinal = JsonConvert.DeserializeObject<ProductInfo>(readresult);
            //return resultadoFinal;
            return new ProductInfo();
        }

        //INVENTADO--------------------------

        public virtual double Currency(CurrencyChange currencyChange)
        {
            //pathController = "";
            //client.BaseAddress = new Uri(BaseUrl);
            //string json = JsonConvert.SerializeObject(currencyChange); //----
            //var httpcontent = new StringContent(json, Encoding.UTF8, "application/json");
            //var response = client.PostAsync(pathController, httpcontent);
            //response.Wait();
            //var result = response.Result;
            //var readresult = result.Content.ReadAsStringAsync().Result;
            //var resultadoFinal = JsonConvert.DeserializeObject<double>(readresult);
            //return resultadoFinal;
            return 1.2;
        }

        public virtual double Shipping(double totalCostOfProducts)
        {
            //pathController = "";
            //client.BaseAddress = new Uri(BaseUrl);
            //var response = client.GetAsync(pathController + totalCostOfProducts);
            //response.Wait();
            //var result = response.Result;
            //var readresult = result.Content.ReadAsStringAsync().Result;
            //var ShippingCost = JsonConvert.DeserializeObject<double>(readresult);
            //return ShippingCost;
            return 3.4;
        }

        public virtual string Payment(PaymentModel paymentModel)
        {
            //pathController = "";
            //client.BaseAddress = new Uri(BaseUrl);
            //string json = JsonConvert.SerializeObject(paymentModel); //----
            //var httpcontent = new StringContent(json, Encoding.UTF8, "application/json");
            //var response = client.PostAsync(pathController, httpcontent);
            //response.Wait();
            //var result = response.Result;
            //var readresult = result.Content.ReadAsStringAsync().Result;
            //var TransactionId = JsonConvert.DeserializeObject<string>(readresult);
            //return TransactionId;
            return "";
        }
    }
}
