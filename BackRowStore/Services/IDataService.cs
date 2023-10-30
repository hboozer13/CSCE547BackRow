

namespace BackRowStore.Services
{
    public interface IDataService
    {
        Task addToCart(string cartId, string itemID, int quantity);

        List<string> getCart(string cartId);
        public Task createCart();

        public bool cartExists(string cartID);


    }
}
