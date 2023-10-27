using System;

public class Cart
{
	Dictionary<string, (string Name, double Price, int Quantity)> itemDictionary = new Dictionary<string, (string, double, int)>
	{
		{"001", ("water bottle", 12.99, 11) },
		{"002", ("apple", 0.99, 23) },
		{"003", ("PS5", 499.99, 2) },
		{"004", ("guitar", 159.99, 6) }
	};
	public string cartID { get; set; }
	public double cartBalance { get; set; }

	public Cart()
	{
		this.cartID = Guid.NewGuid().ToString();
		this.cartBalance = 0;
	}

	
}
