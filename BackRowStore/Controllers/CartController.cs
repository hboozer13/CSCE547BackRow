using BackRowStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackRowStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {

        private readonly IDataService _dataService;

        
        public Dictionary<string, List<string>> carts = new Dictionary<string, List<string>>
        {
            { "1e9d4ff6-22ee-4b4b-bd24-741afa04bf06", new List<string> { "item1", "item2" } }
        };
        

        private readonly ILogger<CartController> _logger;

        /*
        public CartController(ILogger<CartController> logger)
        {
            _logger = logger;
        }
        */
        
        public CartController(IDataService DataService)
        {
            _dataService = DataService;
        }

        [HttpPost("CreateCart", Name = "CreateCart")]
        public IActionResult CreateCart()
        {
            _dataService.createCart();
            return Ok(_dataService.getAllCarts());
        }

        [HttpGet("{cartId}")]
        public IActionResult GetCart(string cartID)
        {
            List<string> cartnew = _dataService.getCart(cartID);
            /*if (cartnew != null) 
            {
                return Accepted(cartnew);
            } else
            {
                return NotFound();
            }*/
            return Ok(cartnew);
        }

        //PUT request to add an item to a cart
        /// <summary>
        /// Takes a cartID, itemID and quantity to locate and add a new item to a specified cart
        /// </summary>
        /// <param name="cartID"></param>
        /// <param name="itemID"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [HttpPut("AddItemToCart", Name = "AddItemToCart")]
        public IActionResult AddItemToCart(string cartID, string itemID, int quantity)
        {
            /*if (!string.IsNullOrEmpty(cartID))
            {
                return BadRequest("cartID is empty.");
            }*/
            /*if (!string.IsNullOrEmpty(itemID))
            {
                return BadRequest("itemID is empty.");
            }*/
            if (quantity == 0)
            {
                return BadRequest("Quantity is empty");
            }
            if (_dataService.cartExists(cartID))
            {
                _dataService.addToCart(cartID, itemID, quantity);
                return Accepted(_dataService.getCart(cartID));
            }
            else
            {
                return NotFound("Cart could not be found.");
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
