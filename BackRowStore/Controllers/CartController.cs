using BackRowStore.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BackRowStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {

        //private readonly IDataService _dataService;
        private readonly BackRowDbContext context;
        private readonly ILogger<CartController> _logger;
        private readonly CartService _cartService;

        public CartController(CartService cartService, BackRowDbContext context)
        {
            _cartService = cartService;
            this.context = context;
        }

        /*
        public CartController(ILogger<CartController> logger)
        {
            _logger = logger;
        }
        */

        /**
        public CartController(IDataService DataService)
        {
            _dataService = DataService;
        }
        **/

        // GET request to create a new cart, 
        [HttpPost("CreateCart", Name = "CreateCart")]
        public IActionResult CreateCart()
        {
            string guid = Guid.NewGuid().ToString();
            List<Item> items = new List<Item>();
            _cartService.CreateCart(guid, items);
            return Ok(guid);
        }

        // GET request to get a cart and return the serialized cart
        [HttpGet("GetCart")]
        public IActionResult GetCart(string cartID)
        {
            Cart cartnew = _cartService.GetCart(cartID);
            if (cartnew != null)
            {
                return Ok(JsonConvert.SerializeObject(cartnew)); 
            }
            else
            {
                return NotFound();
            }
        }

        //PUT request to add an item to a cart
        [HttpPut("AddItemToCart", Name = "AddItemToCart")]
        public IActionResult AddItemToCart(string cartID, string itemID, int quantity)
        {
            if( _cartService.AddItemToCart(cartID, itemID, quantity))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("GetTotals", Name = "GetTotals")]
        public IActionResult GetTotals(string cartID)
        {
            string total = _cartService.GetTotals(cartID);
            return Ok(total);
        }

        [HttpDelete("RemoveItem", Name = "RemoveItem")]
        public IActionResult RemoveItem(string cartID, string itemID)
        {
            Cart cart = _cartService.RemoveItem(cartID, itemID);
            if (cart != null)
            {
                return Ok(JsonConvert.SerializeObject(cart));
            }
            else
            {
                return NotFound("Cart/Item Not Found.");
            }
        }
    }
}
