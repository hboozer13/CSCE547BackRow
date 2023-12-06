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

        [HttpGet("AddtoDatabase", Name = "AddtoDatabase")]
        public IActionResult AddItemToDatabase()
        {
            _itemService.AddNewItem("001", "COOL", 10.99, 8);
            return Ok("Item added to database!");
        }

        // GET request to obtain the items displayed in the store
        [HttpGet("GetAllItems", Name = "GetAllItems")]
        public IEnumerable<Item> GetAllItems()
        {
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