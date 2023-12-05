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
        var returncode = "New item added.";
        var newItem = new Item
        {
            itemID = itemID,
            name = name,
            price = price,
            quantity = quantity
        };

        try
        {
            // TODO: Need to see if the item is already created in the database, if it is, only update the quantity
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

