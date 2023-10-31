using BackRowStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackRowStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {

        private readonly IDataService _dataService;

        

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

        [HttpGet("GetCart")]
        public IActionResult GetCart(string cartID)
        {
            List<string> cartnew = _dataService.getCart(cartID);
            if (cartnew != null)
            {
                return Ok(cartnew);
            }
            else
            {
                return NotFound();
            }
        }

        //PUT request to add an item to a cart
        /// <summary>
        /// Takes a cartID, itemID and quantity to locate and add a new item to a specified cart
        /// </summary>
        /// <param name="cartID"></param>
        /// <param name="itemID"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>git
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
            if (quantity > _dataService.getShop()[itemID].Item3)
            {
                return BadRequest("Not enough item in stock!");
            }
            if (_dataService.cartExists(cartID))
            {
                _dataService.addToCart(cartID, itemID, quantity);
                return Accepted(_dataService.getCart(cartID));
            } else
            {
                return NotFound("Cart could not be found.");
            }
        }

        [HttpGet("GetTotals", Name = "GetTotals")]
        public double GetTotals(string cartID)
        {
            double total = _dataService.getTotals(cartID);
            return total;
        }
    }
}
