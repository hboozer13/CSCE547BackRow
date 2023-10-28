using System;

public class Cart
{
	public string cartID { get; set; }
	public double cartBalance { get; set; }

	public Cart()
	{
		this.cartID = Guid.NewGuid().ToString();
		this.cartBalance = 0;
	}

	
}
