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

    private Dictionary<int, Bundle> bundleCollection = new Dictionary<int, Bundle>();

    private void InitBundles()
    {
        bundleCollection.Add(1, new Bundle("001", new List<string>{"001", "002", "003"}, 499.99));
        bundleCollection.Add(2, new Bundle("002", new List<string> { "005", "009" }, 799.99));
    }

    // Add new cart to database
    public void CreateCart(string cartID, List<string> items)
    {
        var returncode = "New cart added.";
        var newCart = new Cart
        {
            cartID = cartID,
            items = items,
        };

        // Serialize cart to JSON, put in db
        var newCartDB = new CartSerial
        {
            Id = cartID,
            cartSerial = JsonConvert.SerializeObject(newCart).ToString()

        };

        try
        {
            _context.Carts.Add(newCartDB);
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
        var cartnew = new Cart();

        if (cart == null)
        {
            returncode = "Cart not found.";
            Console.WriteLine(returncode);
            return null;
        }

        try
        {
            cartnew = deserializeItem(cart.cartSerial);
        }
        catch (Exception e)
        {
            // Catch exception (any) and log error message, then return error message to user
            Console.WriteLine(e);
            returncode = e.ToString();
            throw;
        }
        Console.WriteLine(returncode);
        return cartnew;
    }

    public Boolean AddItemToCart(string cartID, string itemID, int quantity)
    {
        // LINQ QUERY
        var item = _context.Items.FirstOrDefault(i => i.itemID == itemID);
        var returncode = "Item added to cart.";
        var cart = _context.Carts.Find(cartID);
        var cartnew = deserializeItem(cart.cartSerial);

        // Prevents user from adding something to cart that is out of stock or adding more items than are currently available
        if (item.quantity < quantity || item.quantity == 0)
        {
            return false;
        }
        
        if (cart == null || itemID == null)
        {
            return false;
        }

        try
        {
            for (int i = 0; i < quantity; i++)
            {
                cartnew.items.Add(itemID);
            }
            cart.cartSerial = (JsonConvert.SerializeObject(cartnew).ToString());
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            // Catch exception (any) and log error message, then return error message to user
            Console.WriteLine(e);
            returncode = e.ToString();
            throw;
        }

        if (item != null)
        {
            item.quantity -= quantity;
            _context.SaveChanges();
        }

        return true;
    }

    public string GetTotals(string cartID)
    {
        var returncode = "Totals found.";
        //TODO: update to linq and manually create obj
        var cart = _context.Carts.Find(cartID);
        var cartnew = deserializeItem(cart.cartSerial);
        InitBundles();
        /**
        // Deserialize items list for cart
        cart.items = deserializeItem(cart.itemSerial);
        **/
        double runningTotal = 0;
        double bundleTotal = 0;
        double totalTax = 0;
        string output = "";
        if (cartnew.items == null)
        {
            return "Cart is empty.";
        }
        foreach (string item in cartnew.items)
        {
            var itemnew = _context.Items.Find(item);
            runningTotal += itemnew.price;
        }
        bundleTotal = runningTotal;
        var bundleCheck = copyList(cartnew.items);
        
        // Check cart item list for bundles and apply discounts
        // Bundle i in bundle collection returning nothing
        foreach (Bundle i in bundleCollection.Values)
        {
            List<string> bundleitems = copyList(i.items);
            while (!bundleCheck.Except(bundleitems).Any() && bundleCheck.Count > 0)
            {
                var total = 0.0;
                foreach (string item in bundleitems)
                {
                    total += _context.Items.Find(item).price;
                    bundleCheck.Remove(item);
                }
                bundleTotal -= (total - i.price);
            }
        }

        /**
        foreach (Bundle i in bundleCollection.Values)
        {
            List<string> bundleitems = copyList(i.items);
            while (!bundleCheck.Except(bundleitems).Any())
            {
                var total = 0.0;
                foreach (string item in bundleitems)
                {
                    total += _context.Items.Find(item).price;
                    bundleCheck.Remove(item);
                }
                bundleTotal -= (total - i.price);
            }
        }
        **/
        
        
        totalTax = bundleTotal * 0.07;
        output = "Subtotal: " + Math.Round(runningTotal, 2, MidpointRounding.AwayFromZero) + "\nDiscounted Price: " + Math.Round(bundleTotal,2,MidpointRounding.AwayFromZero) + "\nTax: " + Math.Round(totalTax, 2, MidpointRounding.AwayFromZero) + "\nTotal: " + Math.Round((bundleTotal + totalTax), 2, MidpointRounding.AwayFromZero);
        return output;
    }

    public Cart RemoveItem(string cartID, string itemID)
    {
        var item = _context.Items.FirstOrDefault(i => i.itemID == itemID);
        var returncode = "Item removed from cart.";
        var cart = _context.Carts.Find(cartID);
        var cartnew = deserializeItem(cart.cartSerial);
        /**
        // Deserialize items list for cart
        cart.items = deserializeItem(cart.itemSerial);
        **/
        if (cart == null || itemID == null || _context.Items.Find(itemID) == null)
        {
            return null;
        }

        try
        {
            cartnew.items.Remove(itemID);
            cart.cartSerial = JsonConvert.SerializeObject(cartnew).ToString();
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            // Catch exception (any) and log error message, then return error message to user
            Console.WriteLine(e);
            returncode = e.ToString();
            throw;
        }

        if (item != null)
        {
            item.quantity += 1;
            _context.SaveChanges();
        }

        return cartnew;
    }

    private Cart deserializeItem(string cart)
    {
        if (cart == null)
        {
            return null;
        }
        else
        {
            return JsonConvert.DeserializeObject<Cart>(cart);
        }
    }

    private List<string> copyList(List<string> list)
    {
        List<string> newList = new List<string>();
        foreach (string item in list)
        {
            newList.Add(item);
        }
        return newList;
    }
}