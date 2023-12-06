using System;
using BackRowStore;
public class ItemService 
{
    private readonly BackRowDbContext _context;

    public ItemService(BackRowDbContext context)
    {
        _context = context;
    }

    public void AddNewItem(string itemId, string name, double price, int quantity)
    {
        var returncode = "New item added.";
        var newItem = new Item
        {
            itemID = itemId,
            name = name,
            price = price,
            quantity = quantity
        };

        try
        {
            _context.Items.Add(newItem);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            // Catch exception (any) and log error message, then return error message to user
            Console.WriteLine(e);
            returncode = e.ToString();
            throw;
        }
        

        Console.WriteLine(returncode);
    }
}

