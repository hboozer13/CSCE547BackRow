using System;

namespace BackRowStore 
{ 
	public class Cart
	{
		public string cartID { get; set; }
		public List<string> items { get; set; }
		public double cartBalance { get; set; }

		public Item item { get; set; }

    }
}
