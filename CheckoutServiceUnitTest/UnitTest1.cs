using checkoutservice.Controllers;
using checkoutservice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CheckoutServiceUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        CheckoutController chkoutcontroller = new CheckoutController(true);

        [TestMethod]
        public void CheckoutService_Checkout_Returns()
        {
            UserInfo userinf = new UserInfo();
            userinf.UserId = 987;
            userinf.Name = "Jesus";
            userinf.NumTarget = "485028018302";
            userinf.StreetAddress1 = "ancona";
            userinf.StreetAddress2 = "santa cecilia";
            userinf.City = "Monterrey";
            userinf.Country = "Mexico";
            userinf.ZipCode = "64150";
            userinf.Email = "klamdlamd@gmail.com";
            userinf.CurrencyChange = "MXN";

            OkResult result = (OkResult)chkoutcontroller.CheckPaymentService(userinf);

            Assert.AreEqual(new OkResult().StatusCode,result.StatusCode );
        }
    }
}
