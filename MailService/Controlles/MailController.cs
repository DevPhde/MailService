using MailService.Exceptions;
using MailService.Middlewares;
using MailService.Services;
using Microsoft.AspNetCore.Mvc;

namespace MailService.Controlles
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : Controller
    {
        private readonly IEmailService _emailService;
        public MailController(IEmailService mailService)
        {
            _emailService = mailService;
        }

        [HttpGet]
        [Route("getAll")]
        [AuthorizationMiddleware]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var mails = await _emailService.GetAll();
                return Ok(mails);
            }
            catch (InternalErrorException ex)
            {
                return StatusCode(500, new ObjectResult(ex.Message) { StatusCode = 500 });
            }
        }


        [HttpGet]
        [Route("getMail/{email}")]
        //[AuthorizationMiddleware]
        public async Task<IActionResult> GetByEmail(string email)
        {
            try
            {
                var mailByUserMail = await _emailService.GetMailByUserEmail(email);
                return Ok(mailByUserMail);
            }
            catch (InternalErrorException ex)
            {
                return StatusCode(500, new ObjectResult(ex.Message) { StatusCode = 500 });

            }
        }
    }
}
