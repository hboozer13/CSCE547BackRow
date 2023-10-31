using Microsoft.AspNetCore.Mvc;

namespace BackRowStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckoutController : ControllerBase
    {
        private readonly ILogger<CheckoutController> _logger;

        public CheckoutController(ILogger<CheckoutController> logger)
        {
            _logger = logger;
        }

        [HttpGet("ProcessPayment", Name = "ProcessPayment")]
        public IActionResult ProcessPayment(string cartId, string cardNumber, DateTime exp, string cardholderName, string cvc)
        {

            if (cardNumber.Length == 16 && cvc.Length == 3 && exp.Date > DateTime.Now.Date) {
                return Accepted();
            } else
            {
                return BadRequest();
            }
                
        }
    }
}

