using Microsoft.AspNetCore.Http.HttpResults;
using System.Runtime.InteropServices;

namespace BackRowStore.Services
{
    public class DataService : IDataService
    {
        public Dictionary<string, (string Name, double Price, int Quantity)> itemDictionary;
        public Dictionary<string, List<string>> carts;

        public DataService()
        {
            //Initialize Data into Dictionaries
            itemDictionary = new Dictionary<string, (string, double, int)>
            {
                {"001", ("water bottle", 12.99, 11) },
                {"002", ("apple", 0.99, 23) },
                {"003", ("PS5", 499.99, 2) },
                {"004", ("guitar", 159.99, 6) }
            };

            carts = new Dictionary<string, List<string>>
            {
                { "1e9d4ff6-22ee-4b4b-bd24-741afa04bf06", new List<string> { "001", "002" } }
            };
        }
        public Task addToCart(string cartID, string itemID, int quantity)
        {
            if (itemDictionary.ContainsKey(itemID) && quantity > 0 && quantity <= itemDictionary[itemID].Quantity)
            {
                if (carts.TryGetValue(cartID, out var cart))
                {
                    for (int i =0; i < quantity; i++) 
                    {
                        cart.Add(itemID);
                    }
                    
                }
            }
            return Task.CompletedTask;
        }

        public List<string> getCart(string cartID)
        {
            if (carts.TryGetValue(cartID, out var cart))
            {
                return cart;
            } else
            {
                return null;
            }

        }

        public Task createCart()
        {
            // Creating a unique cart
            string cartId = Guid.NewGuid().ToString();
            carts[cartId] = new List<string>();
            return Task.CompletedTask;
        }

        public int getTotals(string cartID)
        {
            int number = 0;
            for (int i = 0; i < carts[cartID].Count; i++)
            {
                number++;
            }
            return number;
        }

        public bool cartExists(string cartID)
        {
            if (carts.ContainsKey(cartID))
            {
                return true;
            }
            return false;
        }

        public Dictionary<string, List<string>> getAllCarts()
        {
            return carts;
        }

        public Dictionary<string, (string, double, int)> getShop()
        {
            return itemDictionary;
        }

    }
}
