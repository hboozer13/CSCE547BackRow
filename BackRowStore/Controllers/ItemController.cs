using BackRowStore.Services;
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
        private readonly ItemService _itemService;

        // Database of items in the store (Dictionary collection)
        /*Dictionary<string, (string Name, double Price, int Quantity)> itemDictionary = new Dictionary<string, (string, double, int)>
        {
            {"001", ("water bottle", 12.99, 11) },
            {"002", ("apple", 0.99, 23) },
            {"003", ("PS5", 499.99, 2) },
            {"004", ("guitar", 159.99, 6) }
        };*/

        private readonly ILogger<ItemController> _logger;
        private readonly IDataService _dataService;

        /*
        public ItemController(ILogger<ItemController> logger)
        {
            _logger = logger;
        }
        */

        public ItemController(IDataService DataService, ItemService itemService)
        {
            _dataService = DataService;
            _itemService = itemService;
        }

        [HttpGet("AddtoDatabase", Name = "AddtoDatabase")]
        public IActionResult AddItemToDatabase()
        {
            _itemService.AddNewItem("001", "bitch", 10.99, 8);
            return Ok("Item added to database!");
        }

        // GET request to obtain the items displayed in the store
        [HttpGet("GetAllItems", Name = "GetAllItems")]
        public IEnumerable<Item> GetAllItems()
        {
            // Projects each item in the dictionary to a new 'Item' object
            // Use of LINQ Query
            if (_dataService.getShop().Count == 0)
            {
                return Enumerable.Empty<Item>();
            } else
            {
                // Use of LINQ Query
                return _dataService.getShop().Select(item => new Item
                {
                    itemID = item.Key,
                    name = item.Value.Item1,
                    price = item.Value.Item2,

                    quantity = item.Value.Item3
                });
            }
        }
    }
}