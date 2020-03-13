using Microsoft.VisualStudio.TestTools.UnitTesting;
using emailservice.Controllers;
using emailservice.Models;
using emailservice.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Moq;


namespace Email.Tests
{
    [TestClass]
    public class EmailTests
    {

        public Order Order { get; set; }
        //private readonly IConfiguration _configuration;
        //private readonly IHostingEnvironment _hosting;
        //IEmailService _emailService;

        EmailController emailController; 

        public EmailTests(Order order)
        {
            this.Order = order;

        }
        [TestMethod]
        public void SendEmail()
        {
            //var mockFootlooseFSService = new Mock<IFootlooseFSService>();
            //Arrange
            Order.Id = "2133";
            Order.ShippingTrackingId = "90839474949";
            Order.Customer = new Customer() {
                Id = "1",
                Email = "edgarmc100@gmail.com",
                Name = "Edgar Morales"
            };
            Order.ShippingAddress = new ShippingAddress() {
                Country = "México",
                City = "Monterrey N,L.",
                StreetAddress1 = "Martires",
                StreetAddress2 = "Santa Catarina",
                ZipCode = "29660"
            };
            Order.Items = new System.Collections.Generic.List<Item>() {
                new Item (){
                    IdProduct = "1664774",
                    Name = "Patines",
                    Description = "Patines hechos de plastico con ruedas resistentes al agua",
                    Image = "https://images-na.ssl-images-amazon.com/images/I/81d09Kj4K3L._AC_SX679_.jpg",
                    Quantity = 1
                },

                new Item() {
                    IdProduct = "188345",
                    Name = "iMac",
                    Description = "Apple - iMac MMQA2E/A de 21.5\"",
                    Quantity = 2,
                    Image = "https://images.unsplash.com/photo-1527443224154-c4a3942d3acf?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=750&q=80"
                },

                new Item(){
                    IdProduct = "190283",
                    Name =  "MacBook Pro 15\"",
                    Description = "Apple - MacBook Pro (último modelo) 13\"",
                    Quantity=1,
                    Image = "https://pisces.bbystatic.com/image2/BestBuy_MX/images/products/1000/1000222172_sa.jpg"
                }

            };

            //Act

            //Assert
        }
    }
}
