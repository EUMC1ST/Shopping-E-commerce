using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using emailservice.Models;
using emailservice.Services;

namespace emailservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _iEmailService;

        public EmailController(IEmailService emailService)
        {
            _iEmailService = emailService;
        }
        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Order order)
        {
            if (ModelState.IsValid)
            {
                await _iEmailService.SendEmail(order);
                return Ok();
            }

            return BadRequest();
        }
    }
}
