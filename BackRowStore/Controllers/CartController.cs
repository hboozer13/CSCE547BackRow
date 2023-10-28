using Microsoft.AspNetCore.Mvc;

namespace BackRowStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ILogger<CartController> _logger;

        public CartController(ILogger<CartController> logger)
        {
            _logger = logger;
        }

        [HttpPost("CreateCart", Name = "CreateCart")]
        public IActionResult CreateCart()
        {
            // Creating a unique cart
            string cartId = Guid.NewGuid().ToString();
            var cart = new Cart(cartId);
            return Ok("Cart was Created");
        }

        [HttpGet("GetCart", Name = "GetCart")]
        public IEnumerable<Item> GetCart()
        {
            return Enumerable.Range(1, 5).Select(index => new Item
            {
                name = "ITEM",
            })
            .ToArray();
        }

        [HttpGet("GetTotals", Name = "GetTotals")]
        public IEnumerable<Item> GetTotals()
        {
            return Enumerable.Range(1, 5).Select(index => new Item
            {
                name = "ITEM",
            })
            .ToArray();
        }
    }
}
