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
            //Obtenemos de el objeto regresado Cart obtenemos los ID de la lista Productos almacenada en Items
            List<ProductInfo> productInfo = cartList.Productos.Select(x => Api.ProductCatalog(x.ProductId)).ToList();

            List<CurrencyChange> currencyChanges = productInfo.Select(x => new CurrencyChange()
                                                                {
                                                                    CurrencyCode = x.Price.CurrencyCode,
                                                                    Units = x.Price.Units,
                                                                    Nano = x.Price.Nano,
                                                                    CurrencyType = User.CurrencyChange
                                                                }).ToList();

            List<double> priceOfProducts = currencyChanges.Select(x => Api.Currency(x)).ToList();
            List<int> quantity = cartList.Productos.Select(x => x.Quantity).ToList();

            double totalCostOfProducts = priceOfProducts.Select((x, i) => x * quantity[i]).Sum();

            // costo de envio
            double shippingCost = Api.Shipping(totalCostOfProducts);
            // total de compra
            double totalCost = totalCostOfProducts + shippingCost;

            //payment
            PaymentModel paymentModel = new PaymentModel()
            {
                TargetNumber = User.NumTarget,
                TotalToPay = totalCost
            };
            string TransactionId = Api.Payment(paymentModel);
            //-------------

            //Email

            return Ok();
        }
    }
}