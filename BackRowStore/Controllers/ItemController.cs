using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace BackRowStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
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

        

        [HttpGet("GetAllItems", Name = "GetAllItems")]
        public IEnumerable<Item> GetAllItems()
        {
            return itemDictionary.Select(item => new Item
            {
                itemID = item.Key,
                name = item.Value.Name,
                price = item.Value.Price,
                quantity = item.Value.Quantity
            });
        }

        [HttpGet("AddItemToCart", Name = "AddItemToCart")]
        public IEnumerable<(string Name, double Price, int Quantity)> AddItemToCart()
        {
            return itemDictionary.Values;
        }
    }
}