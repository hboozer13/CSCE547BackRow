using System;

namespace BackRowStore 
{ 
	public class Cart
	{
		public string cartID { get; set; }
		public List<Item> items { get; set; }
		public double cartBalance { get; set; }

		public Item item { get; set; }

		public string itemSerial { get; set; }

    }
}
