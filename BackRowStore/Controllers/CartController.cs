using Microsoft.AspNetCore.Mvc;

namespace BackRowStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        public Dictionary<string, List<string>> carts = new Dictionary<string, List<string>>
        {
            { "1e9d4ff6-22ee-4b4b-bd24-741afa04bf06", new List<string> { "item1", "item2" } }
        };

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
            carts[cartId] = new List<string>();

            return Ok("Cart was Created. Your Cart ID is: "+ cartId);
        }

        [HttpGet("{cartId}")]
        public IActionResult GetCart(string cartId)
        {
            if (carts.TryGetValue(cartId, out var cart)) 
            {
                return Accepted(cart);
            } else
            {
                return NotFound();
            }
            
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
