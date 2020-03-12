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
            Cart cartList;
            List<ProductInfo> productInfo = new List<ProductInfo>();
            List<CurrencyChange> currencyChanges = new List<CurrencyChange>();
            List<double> priceOfProducts = new List<double>();
            try
            {
                 cartList = Api.CartService(User.UserId);
            }catch(Exception e)
            {
                return Content("No ha sido posible conectar con el api de cart");
            }
            if(cartList.Productos==null || cartList.Productos.Count == 0)
            {
                return Content("No se han agregado Items al Cart");
            }
            //Obtenemos de el objeto regresado Cart obtenemos los ID de la lista Productos almacenada en Items
            try
            {
                 productInfo = cartList.Productos.Select(x => Api.ProductCatalog(x.ProductId)).ToList();
            }catch(Exception e)
            {
                return Content("No se ha podido conectar con product catalog");
            }
            try
            {
                 currencyChanges = productInfo.Select(x => new CurrencyChange()
                {
                    CurrencyCode = x.Price.CurrencyCode,
                    Units = x.Price.Units,
                    Nano = x.Price.Nano,
                    CurrencyType = User.CurrencyChange
                }).ToList();
            }catch(Exception e)
            {
                return Content("No eres tu, soy yo(Error en sacar los precios del productInfo)");
            }
            try
            {
                priceOfProducts = currencyChanges.Select(x => Api.Currency(x)).ToList();
            }catch(Exception e)
            {
                return Content("No ha sido posible conectar con CurrencyChange");
            }

            List<int> quantity = cartList.Productos.Select(x => x.Quantity).ToList();
            double totalCostOfProducts = priceOfProducts.Select((x, i) => x * quantity[i]).Sum();

            // costo de envio *******Puede haber un cambio al mandar los parametros en Shipping*********
            double shippingCost = Api.Shipping(totalCostOfProducts);

            // total de compra
            double totalCost = totalCostOfProducts + shippingCost;

            //payment
            PaymentModel paymentModel = new PaymentModel()
            {
                TargetNumber = User.NumTarget,
                TotalToPay = totalCost
            };
            try
            {
                string TransactionId = Api.Payment(paymentModel);
            }catch(Exception e)
            {
                return Content("Error at conecting to payment");
            }
            //-------------

            //Email
            Order CustomerOrder = new Order();
            CustomerOrder.Id = 000001;//Ocupamos el ID de la compra o lo generamos nosotros?
            CustomerOrder.Customer.Id = User.UserId;
            CustomerOrder.Customer.Name = User.Name;
            CustomerOrder.Customer.Email = User.Email;
            CustomerOrder.ShippingTrackingId = "";//Aun no se implementara
            CustomerOrder.ShippingAddress.StreetAddress1 = User.StreetAddress1;
            CustomerOrder.ShippingAddress.StreetAddress2 = User.StreetAddress2;
            CustomerOrder.ShippingAddress.City = User.City;
            CustomerOrder.ShippingAddress.Country = User.Country;
            CustomerOrder.ShippingAddress.ZipCode = User.ZipCode;
            CustomerOrder.Items = cartList.Productos;
            try
            {
                ActionResult status = Api.Email(CustomerOrder);
            }catch(Exception e)
            {
                return Content("Error at send Email");
            }
            return Ok();
        }
    }
}