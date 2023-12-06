using BackRowStore.Services;
using BackRowStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Xml.Linq;


namespace BackRowStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ItemService _itemService;
        private readonly ILogger<ItemController> _logger;
        private readonly IDataService _dataService;
        private readonly BackRowDbContext _context;

        public ItemController(IDataService DataService, ItemService itemService, BackRowDbContext context)
        {
            _dataService = DataService;
            _itemService = itemService;
            _context = context;
        }

        /*itemDictionary = new Dictionary<string, (string, double, int)>
            {
                {"001", ("water bottle", 12.99, 11) },
                {"002", ("apple", 0.99, 23) },
                {"003", ("PS5", 499.99, 2) },
                {"004", ("guitar", 159.99, 6) },
                {"005", ("mr krabs plushie", 999.99, 1) },
                {"006", ("skinamarink poster", 19.99, 7) },
                {"007", ("zaxbys nibbler meal", 8.99, 22) },
                {"008", ("snoop dogg cookbook", 23.99, 10) },
                {"009", ("magic mike movie ticket (premium experience)", 299.99, 1) },
                {"010", ("capgemini fast pass", 10000, 1) },
                {"011", ("florida gators calculator", 99.99, 2) },
                {"012", ("haydens room at cayce cove 222", 3.99, 1) }
            };*/

        [HttpGet("AddtoDatabase", Name = "AddtoDatabase")]
        public IActionResult AddItemToDatabase()
        {
            _itemService.AddNewItem("001", "water bottle", 12.99, 11);
            _itemService.AddNewItem("002", "apple", 0.99, 23);
            _itemService.AddNewItem("003", "PS5", 499.99, 2);
            _itemService.AddNewItem("004", "guitar", 159.99, 6);
            _itemService.AddNewItem("005", "mr krabs plushie", 999.99, 1);
            _itemService.AddNewItem("006", "skinamarink poster", 19.99, 7);
            _itemService.AddNewItem("007", "zaxbys nibbler meal", 8.99, 22);
            _itemService.AddNewItem("008", "snoop dogg cookbook", 23.99, 10);
            _itemService.AddNewItem("009", "magic mike movie ticket (premium experience)", 299.99, 1);
            _itemService.AddNewItem("010", "capgemini fast pass", 10000, 1);
            _itemService.AddNewItem("011", "florida gators calculator", 99.99, 2);
            _itemService.AddNewItem("012", "haydens room at cayce cove 222", 3.99, 1);
            return Ok("Item added to database!");
        }

        // GET request to obtain the items displayed in the store
        [HttpGet("GetAllItems", Name = "GetAllItems")]
        public IEnumerable<Item> GetAllItems()
        {
            // LINQ Query
            var items = from item in _context.Items
                        select new Item
                        {
                            itemID = item.itemID,
                            name = item.name,
                            price = item.price,
                            quantity = item.quantity
                        };

            if (!items.Any())
            {
                return Enumerable.Empty<Item>();
            }
            else
            {
                return items.ToList();
            }
        }
    }
}