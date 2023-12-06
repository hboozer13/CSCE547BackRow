// PHASE ONE 


using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Runtime.InteropServices;

namespace BackRowStore.Services
{
    public class DataService : IDataService
    {
        public Dictionary<string, (string Name, double Price, int Quantity)> itemDictionary;
        public Dictionary<string, List<string>> carts;
        public Dictionary<string, List<string>> bundles;

        public DataService()
        {

            itemDictionary = new Dictionary<string, (string, double, int)>
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
            };

            // This will keep track of existing carts
            carts = new Dictionary<string, List<string>>();

            /*bundles = new Dictionary<string, List<string>>
            {
                { "Magic Krabs Bundle", new List<string> {"005", "009"} }
            };*/
        }

        public Task addToCart(string cartID, string itemID, int quantity)
        {
            if (itemDictionary.ContainsKey(itemID) && quantity > 0 && quantity <= itemDictionary[itemID].Quantity)
            {
                if (carts.TryGetValue(cartID, out var cart))
                {
                    for (int i = 0; i < quantity; i++)
                    {
                        cart.Add(itemID);
                    }

                }
            }
            return Task.CompletedTask;
        }

        // 
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

        public string getTotals(string cartID)
        {
            double runningTotal = 0;
            double bundleTotal = 0;
            double totalTax = 0;
            string output = "";
            foreach (string item in carts[cartID])
            {
                runningTotal += itemDictionary[item].Price;
            }
            bundleTotal = runningTotal;
            // Had to hard code in value, implementation refuses to accept an object in a dictionary so I can't make it dynamic. Will fix in part 2 of the project.
            if (carts[cartID].Contains(bundles["Magic Krabs Bundle"][0]) && carts[cartID].Contains(bundles["Magic Krabs Bundle"][1]))
            {
                var id1 = bundles["Magic Krabs Bundle"][0];
                var id2 = bundles["Magic Krabs Bundle"][1];
                bundleTotal = runningTotal - (itemDictionary[id1].Price + itemDictionary[id2].Price) + 750.00;
            }
            totalTax = (bundleTotal * 0.07) + bundleTotal;

            output += "runningTotal: " + runningTotal + "\nbundleTotal: " + bundleTotal + "\ntotalTax: " + totalTax;
            return output;
        }

        // validates if a cart exists given an ID
        public bool cartExists(string cartID)
        {
            if (carts.ContainsKey(cartID))
            {
                return true;
            }
            return false;
        }

        // returns all the carts that exist in the program
        public Dictionary<string, List<string>> getAllCarts()
        {
            return carts; 
        }

        // returns all available item in the store
        public Dictionary<string, (string, double, int)> getShop()
        {
            return itemDictionary;
        }

    }
}
