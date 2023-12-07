using BackRowStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackRowStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckoutController : ControllerBase
    {
        //private readonly ILogger<CheckoutController> _logger;
        private readonly CartService _cartService;


        public CheckoutController(CartService cartService)
        {
            _cartService = cartService;
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
            if (cardNumber.Length == 16 && cvc.Length == 3 && exp.Date > DateTime.Now.Date && _cartService.cartExists(cartId)) {
                _cartService.clearCart(cartId);
                return Accepted();
            } else
            {
                return BadRequest();
            }
                
        }
    }
}

