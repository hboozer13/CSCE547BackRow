using System;
using BackRowStore;
public class ItemService 
{
    private readonly BackRowDbContext _context;

    public ItemService(BackRowDbContext context)
    {
        _context = context;
    }

    public void AddNewItem(string itemID, string name, double price, int quantity)
    {
        var newItem = new Item
        {
            itemID = itemID,
            name = name,
            price = price,
            quantity = quantity
        };

        _context.Items.Add(newItem);
        _context.SaveChanges();

        Console.WriteLine("New item added.");
    }
}

