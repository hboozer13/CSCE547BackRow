using BackRowStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackRowStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryService _inventoryService;
        private readonly ILogger<ItemController> _logger;
        private readonly IDataService _dataService;
        private readonly BackRowDbContext _context;

        public InventoryController(InventoryService inventoryService, BackRowDbContext context)
        {
            _inventoryService = inventoryService;
            _context = context;

        }

        [HttpPut("UpdateStock")]
        public void UpdateStock(string itemId, int newStock)
        {
            _inventoryService.UpdateQuantity(itemId, newStock);
        }

        [HttpPut("ChangePrice")]
        public void ChangePrice(string itemId, double newPrice)
        {
            _inventoryService.UpdatePrice(itemId, newPrice);
        }
    }
}
