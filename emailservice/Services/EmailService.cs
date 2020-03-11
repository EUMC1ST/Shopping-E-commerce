using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using emailservice.Models;
using System.Configuration;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Web.Http;
using Microsoft.AspNetCore.Hosting;


namespace emailservice.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _iHostingEnvironment;

        public EmailService(IConfiguration configuration, IHostingEnvironment iHostingEnvironment)
        {
            _configuration = configuration;
            _iHostingEnvironment = iHostingEnvironment;
        }

        public async Task SendEmail(Order order)
        {
            using (var client = new SmtpClient())
            {
                var credential = new NetworkCredential()
                {
                    UserName = _configuration["Email:Email"],
                    Password = _configuration["Email:Password"]
                };
                client.Credentials = credential;
                client.Host = _configuration["Email:Host"];
                client.Port = int.Parse(_configuration["Email:Port"]);

                //Enable encryption-based Internet security protocol
                client.EnableSsl = true;

                //Email message used to send email
                using (var emailMessage = new MailMessage())
                {
                    emailMessage.To.Add(new MailAddress(order.Customer.Email));
                    emailMessage.From = new MailAddress(_configuration["Email:Email"]);
                    emailMessage.Subject = "Shopping order confirmation";
                    emailMessage.Body = SetEmailBody(order);
                    client.Send(emailMessage);
                }
            }

            await Task.CompletedTask;
        }

        public string SetEmailBody(Order order)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(_iHostingEnvironment.ContentRootPath + "/Assets/template.html"))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{order.customer.name}", order.Customer.Name);
            body = body.Replace("{order.id}", order.Id.ToString());
            //body = body.Replace("{order.shipping_tracking_id}", order.ShippingTrackingId );
            body = body.Replace("{order.shipping_address.street_address_1}", order.ShippingAddress.StreetAddress1);
            body = body.Replace("{order.shipping_address.street_address_2}", order.ShippingAddress.StreetAddress2);
            body = body.Replace("{order.shipping_address.country}", order.ShippingAddress.Country);
            body = body.Replace("{order.shipping_address.city}", order.ShippingAddress.City);
            body = body.Replace("{order.shipping_address.zip_code}", order.ShippingAddress.ZipCode);
            return body;
        }
    }
}
