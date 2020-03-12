using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace checkoutservice
{
    public class HttpRequests
    {
        HttpClient client = new HttpClient();
        readonly string BaseUrl = "alguna/url/delasapis";
        // metodo<quetipo>(entradas)
        public output TheGet<output>(string pathController) 
        {
            client.BaseAddress = new Uri(BaseUrl);
            var response = client.GetAsync(pathController);
            response.Wait();
            var result = response.Result;
            var readresult = result.Content.ReadAsStringAsync().Result;
            var resultadoFinal = JsonConvert.DeserializeObject<output>(readresult);
            return resultadoFinal;
        }


    }
}
