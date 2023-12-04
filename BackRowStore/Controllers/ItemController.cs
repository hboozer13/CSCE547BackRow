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
        private readonly BackRowDbContext context;

        public ItemController(IDataService DataService, ItemService itemService, BackRowDbContext context)
        {
            _dataService = DataService;
            _itemService = itemService;
            this.context = context;
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