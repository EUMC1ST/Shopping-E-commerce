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
                Api = new ApiCallsMock();
            }
        }
        [HttpPost]
        [Route("api/checkout/")]
        //En vez de recibir un UserID, recibir un objeto de tipo User.
        public ActionResult CheckPaymentService(UserInfo User)
        {
            Cart cartList;
            List<ProductInfo> productInfo = new List<ProductInfo>();
            List<CurrencyChange> currencyChanges = new List<CurrencyChange>();
            ShippingAddress address = new ShippingAddress() { City = User.City, Country = User.Country, StreetAddress1 = User.StreetAddress1, StreetAddress2 = User.StreetAddress2, ZipCode = User.ZipCode };
            List<double> priceOfProducts = new List<double>();
            try
            {
                cartList = Api.CartService(User.UserId);
            }
            catch (Exception e)
            {
                return Content("No ha sido posible conectar con el api de cart");
            }
            if (cartList.Productos == null || cartList.Productos.Count == 0)
            {
                return Content("No se han agregado Items al Cart");
            }
            //Obtenemos de el objeto regresado Cart obtenemos los ID de la lista Productos almacenada en Items
            try
            {
                productInfo = cartList.Productos.Select(x => Api.ProductCatalog(x.idProduct)).ToList();
            }
            catch (Exception e)
            {
                return Content("No se ha podido conectar con product catalog");
            }

            currencyChanges = productInfo.Select(x => new CurrencyChange()
            {
                CurrencyCode = x.priceUsd.currencyCode,
                Units = x.priceUsd.units,
                Nano = x.priceUsd.nanos,
                CurrencyType = User.CurrencyExchange
            }).ToList();
            try
            {
                priceOfProducts = currencyChanges.Select(x => Api.Currency(x)).ToList();
            }
            catch (Exception e)
            {
                return Content("No ha sido posible conectar con CurrencyChange");
            }
            List<int> quantity = cartList.Productos.Select(x => x.quantity).ToList();
            double totalCostOfProducts = priceOfProducts.Select((x, i) => x * quantity[i]).Sum();
            //Shipping Methods

            var shippingTrackingID = Api.ShippingTracking(address);
            ShippingCost costoenvio = new ShippingCost() { calculatedShippingCost = totalCostOfProducts };
            ShippingCost shippingCost = Api.Shipping(costoenvio); // FALTA ESTO----------

            // total de compra
            double totalCost = totalCostOfProducts + shippingCost.calculatedShippingCost;
            //payment
            PaymentModel paymentModel = new PaymentModel()
            {
                TargetNumber = User.Credit_number_target,
                TotalToPay = totalCost
            };
            try
            {
                string TransactionId = Api.Payment(paymentModel);
            }
            catch (Exception e)
            {
                return Content("Error at conecting to payment");
            }
            //Email
            Order CustomerOrder = new Order();
            CustomerOrder.Customer = new Customer();
            CustomerOrder.ShippingAddress = new ShippingAddress();
            CustomerOrder.Id = Guid.NewGuid().ToString();
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
                ActionResult status = Api.Email(CustomerOrder); //EMAIL NO JALA
                if (status is null || status is EmptyResult)
                {
                    return Content("No se pudo mandar correo");
                }
            }
            catch (Exception e)
            {
                return Content("Error at send Email");
            }
            //tenemos que regresar el id de la compra y el shippingtrackingid, el cargo de envio a pagar y total a pagar
            return Ok();
        }
    }
}