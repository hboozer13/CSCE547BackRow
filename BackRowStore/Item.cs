using System;
using System.Diagnostics;

namespace BackRowStore 
{
    public class Item
    {
        public string name { get; set; }
        public double price { get; set; }
        public string itemID { get; set; }
        public int quantity { get; set; }

        public Item()
        {
            this.name = name;
            this.price = price;
            this.itemID = Guid.NewGuid().ToString();
            this.quantity = quantity;
        }

    }
}
