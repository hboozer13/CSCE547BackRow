using System;

namespace BackRowStore 
{ 
	public class Cart
	{
		public string cartID { get; set; }
		public List<Item> items { get; set; }
		public double cartBalance { get; set; }

    }
}
