using System;
using System.Net;
using System.Threading.Tasks;
using Demo.EmailNotifications.Business;
using Demo.EmailNotifications.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.EmailNotification.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class EmailController : Controller
    {
        private readonly IEmailSender _emailSender;
        public EmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        // POST api/Email
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody]MailMessage mailMessage)
        {
            BaseResponse response;
            try
            {
                response = await _emailSender.SendEmailAsync(mailMessage);

                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response = new BaseResponse(false, ex.Message);
                return new ObjectResult(response)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                };
            }
        }
    }
}
