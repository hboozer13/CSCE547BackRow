using BackRowStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackRowStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckoutController : ControllerBase
    {
        private readonly ILogger<CheckoutController> _logger;
        private readonly IDataService _dataService;


        public CheckoutController(IDataService DataService)
        {
            _dataService = DataService;
        }
        /*
        public CheckoutController(ILogger<CheckoutController> logger)
        {
            _logger = logger;
        }
        */

        [HttpGet("ProcessPayment", Name = "ProcessPayment")]
        public IActionResult ProcessPayment(string cartId, string cardNumber, DateTime exp, string cardholderName, string cvc)
        {

            if (cardNumber.Length == 16 && cvc.Length == 3 && exp.Date > DateTime.Now.Date && _dataService.cartExists(cartId)) {
                return Accepted();
            } else
            {
                return BadRequest();
            }
                
        }
    }
}

