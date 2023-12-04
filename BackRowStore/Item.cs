using System;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;

namespace BackRowStore 
{
    public class Item
    {
        public string itemID { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
    }
}
