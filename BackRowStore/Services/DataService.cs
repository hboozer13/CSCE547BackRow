﻿using Microsoft.AspNetCore.Http.HttpResults;
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

            carts = new Dictionary<string, List<string>>
            {
                { "1e9d4ff6-22ee-4b4b-bd24-741afa04bf06", new List<string> { "005", "008" } }
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

        public double getTotals(string cartID)
        {
            double runningTotal = 0;
            foreach (string item in carts[cartID])
            {
                runningTotal += itemDictionary[item].Price;
            }
            return runningTotal;
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
