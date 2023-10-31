

namespace BackRowStore.Services
{
    public interface IDataService
    {
        Task addToCart(string cartId, string itemID, int quantity);

        List<string> getCart(string cartID);
        public Task createCart();

        public bool cartExists(string cartID);

        public double getTotals(string cartID);

        public Dictionary<string, List<string>> getAllCarts();

        public Dictionary<string, (string, double, int)> getShop();

    }
}
