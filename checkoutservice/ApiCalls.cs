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
        HttpClient client = new HttpClient();
        string BaseUrl = "alguna/url/delasapis";
        string pathController;

        public virtual Cart CartService(int userID)
        {
            pathController = "api/cart?userId=";
            client.BaseAddress = new Uri(BaseUrl);
            var response = client.GetAsync(pathController+userID);
            response.Wait();
            var result = response.Result;
            var readresult = result.Content.ReadAsStringAsync().Result;
            var resultadoFinal = JsonConvert.DeserializeObject<Cart>(readresult);
            return resultadoFinal;
        }
        public virtual ProductInfo ProductCatalog(int productID)
        {
            pathController = "api/productCatalogService/";
            client.BaseAddress = new Uri(BaseUrl);
            var response = client.GetAsync(pathController + productID);
            response.Wait();
            var result = response.Result;
            var readresult = result.Content.ReadAsStringAsync().Result;
            var resultadoFinal = JsonConvert.DeserializeObject<ProductInfo>(readresult);
            return resultadoFinal;
        }
    }
}
