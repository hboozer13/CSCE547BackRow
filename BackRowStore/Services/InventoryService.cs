namespace BackRowStore.Services
{
    public class InventoryService
    {
        private readonly BackRowDbContext _context;

        public InventoryService(BackRowDbContext context)
        {
            _context = context;
        }

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
