using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using checkoutservice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace checkoutservice.Controllers
{
    //[Route("api/[controller]/[action]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        ApiCalls Api;
        public CheckoutController(bool test = false)
        {
            if (test == false)
            {
                Api = new ApiCalls();
            }
            else
            {
                //AGARRA EL MOCK
            }
        }
        [HttpPost]
        [Route("api/checkout/checkPaymentService")]
        //En vez de recibir un UserID, recibir un objeto de tipo User.
        public ActionResult CheckPaymentService(UserInfo User)
        {
            var cartList = Api.CartService(User.UserId);
            List<ProductInfo> productInfo = new List<ProductInfo>();
            List<double> priceOfProducts = new List<double>();
            //Obtenemos de el objeto regresado Cart obtenemos los ID de la lista Productos almacenada en Items
            var productsID = cartList.Productos.Select(x => x.ProductId).ToList();
            for(int i = 0; i <= productsID.Count; i++)
            {
                //Mandamos llamar la api de ProductCatalogService mandando como parametro los id obtenidos del carrito de compra(Api Cart)
                productInfo.Add(Api.ProductCatalog(productsID[i]));
            }
            for(int i = 0; i<= productInfo.Count; i++)
            {
                //Se manda llamar la api de Currency para saber el precio de cada producto que obtuvimos de ProductCatalogService
                //priceOfProducts.Add(Api.Currency(productInfo[i].Price));
            }
            //Faltaria multiplicar el quantity que se tiene en la lista de Cart con la lista de priceOfProducts que es el precio que nos regresa Currency

            //falta obtener el monto a pagar por envio a la ciudad que corresponde pero shipping no lleva nada ni tiene documentacion xD c va* 

            //Despues de obtener el monto de envio realizamos las operaciones en otro metodo que lleve por nombre suma

            return Ok();
        }
    }
}