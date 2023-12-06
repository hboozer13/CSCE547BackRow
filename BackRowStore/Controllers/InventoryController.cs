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
        public IActionResult UpdateStock(string itemId, int newStock)
        {
            if (_context.Items.Any(i => i.itemID == itemId))
            {
                if (newStock < 0)
                {
                    return BadRequest("That is not a valid quantity");
                } else
                {
                    _inventoryService.UpdateQuantity(itemId, newStock);
                    return Ok("Stock successfully updated!");
                }
            } else
            {
                return BadRequest("That item ID does not exist");
            }
            
        }

        [HttpPut("ChangePrice")]
        public IActionResult ChangePrice(string itemId, double newPrice)
        {
            if (_context.Items.Any(i => i.itemID == itemId))
            {
                if (newPrice < 0)
                {
                    return BadRequest("That is not a valid price");
                }
                else
                {
                    _inventoryService.UpdatePrice(itemId, newPrice);
                    return Ok("Stock successfully updated!");
                }
            }
            else
            {
                return BadRequest("That item ID does not exist");
            }
        }
    }
}
