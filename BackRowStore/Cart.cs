using System;

namespace BackRowStore 
{ 
	public class Cart
	{
		public string cartID { get; set; }
		public List<Item> items { get; set; }
		public double cartBalance { get; set; }

        public Cart(string cartId)
        {
            cartID = cartId;
            items = new List<Item>();
        }
    }
}
