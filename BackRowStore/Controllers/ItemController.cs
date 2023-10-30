using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;
using System.Xml.Linq;


namespace BackRowStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        // Database of items in the store (Dictionary collection)
        Dictionary<string, (string Name, double Price, int Quantity)> itemDictionary = new Dictionary<string, (string, double, int)>
        {
            {"001", ("water bottle", 12.99, 11) },
            {"002", ("apple", 0.99, 23) },
            {"003", ("PS5", 499.99, 2) },
            {"004", ("guitar", 159.99, 6) }
        };

        private readonly ILogger<ItemController> _logger;

        public ItemController(ILogger<ItemController> logger)
        {
            _logger = logger;
        }

        
        // GET request to obtain the items displayed in the store
        [HttpGet("GetAllItems", Name = "GetAllItems")]
        public IEnumerable<Item> GetAllItems()
        {
            // Projects each item in the dictionary to a new 'Item' object
            // Use of LINQ Query
            if (itemDictionary.Count == 0)
            {
                return Enumerable.Empty<Item>();
            } else
            {
                // Use of LINQ Query
                return itemDictionary.Select(item => new Item
                {
                    itemID = item.Key,
                    name = item.Value.Name,
                    price = item.Value.Price,
                    quantity = item.Value.Quantity
                });
            }
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
            if (!string.IsNullOrEmpty(cartID)) 
            {
                return BadRequest("cartID is empty.");
            }
            if (!string.IsNullOrEmpty(itemID)) 
            {
                return BadRequest("itemID is empty.");
            }
            if (quantity == 0) 
            {
                return BadRequest("Quantity is empty");
            }
            if (itemDictionary.Count == 0) 
            {
                return BadRequest("No items in database");
            }
            // TODO: Need to find a way to call cart dictionary
            if (!CartController.carts.ContainsKey(cartID)) 
            {
                return BadRequest("Cart not found");
            }

            
            return Ok();
        }
    }
}