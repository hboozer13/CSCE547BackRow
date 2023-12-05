using System;
using System.Linq;
using BackRowStore;
using Newtonsoft.Json;
public class CartService
{
    private readonly BackRowDbContext _context;

    public CartService(BackRowDbContext context)
    {
        _context = context;
    }

    // Add new cart to database
    public void CreateCart(string cartID, List<Item> items)
    {
        var returncode = "New cart added.";
        var newCart = new Cart
        {
            cartID = cartID,
            // Serialize items list
            itemSerial = JsonConvert.SerializeObject(items, Formatting.Indented)
        };

        try
        {
            _context.Carts.Add(newCart);
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

    public Cart GetCart(string cartID)
    {
        var returncode = "Cart found.";
        var cart = _context.Carts.Find(cartID);

        if (cart == null)
        {
            returncode = "Cart not found.";
            Console.WriteLine(returncode);
            return null;
        }

        Console.WriteLine(returncode);
        return cart;
    }

    public Boolean AddItemToCart(string cartID, string itemID, int quantity)
    {
        var returncode = "Item added to cart.";
        var cart = _context.Carts.Find(cartID);
        cart.items = deserializeItem(cart.itemSerial);
        //TODO: Update quantity after adding to cart
        var item = _context.Items.Find(itemID);
        if (cart == null || item == null)
        {
            return false;
        }

        try
        {
            cart.itemSerial = (JsonConvert.SerializeObject(item, Formatting.Indented));
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            // Catch exception (any) and log error message, then return error message to user
            Console.WriteLine(e);
            returncode = e.ToString();
            throw;
        }

        return true;
    }

    public string GetTotals(string cartID)
    {
        var returncode = "Totals found.";
        //TODO: update to linq and manually create obj
        var cart = _context.Carts.Find(cartID);
        // Deserialize items list for cart
        cart.items = deserializeItem(cart.itemSerial);
        double runningTotal = 0;
        double bundleTotal = 0;
        double totalTax = 0;
        string output = "";
        if (cart.items == null)
        {
            return "Cart is empty.";
        }
        foreach (Item item in cart.items)
        {
            runningTotal += item.price;
        }
        //TODO: Add bundle logic
        totalTax = bundleTotal * 0.07;
        output = "Subtotal: " + bundleTotal + "\nTax: " + totalTax + "\nTotal: " + (bundleTotal + totalTax);
        return output;
    }

    public Cart RemoveItem(string cartID, string itemID)
    {
        var returncode = "Item removed from cart.";
        var cart = _context.Carts.Find(cartID);
        var item = _context.Items.Find(itemID);
        // Deserialize items list for cart
        cart.items = deserializeItem(cart.itemSerial);
        if (cart == null || item == null)
        {
            return null;
        }

        try
        {
            cart.items.Remove(item);
            cart.itemSerial = JsonConvert.SerializeObject(cart.items, Formatting.Indented);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            // Catch exception (any) and log error message, then return error message to user
            Console.WriteLine(e);
            returncode = e.ToString();
            throw;
        }
        return cart;
    }

    private List<Item> deserializeItem(string items)
    {
        if (items == null)
        {
            return null;
        }
        else
        {
            Console.WriteLine(JsonConvert.DeserializeObject<List<Item>>(items));

            return JsonConvert.DeserializeObject<List<Item>>(items);
        }
    }
}