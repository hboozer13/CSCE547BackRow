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

        [HttpPut("UpdateStock/{itemId}")]
        public void UpdateQuantity(string itemId, int newStock)
        {
            var item = _context.Items.FirstOrDefault(i => i.itemID == itemId);
            if (item != null)
            {
                item.quantity = newStock;
                _context.SaveChanges();
            }
        }
    }
}
