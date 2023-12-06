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
        bundleCollection.Add(1, new Bundle("002", new List<string> { "005", "009" }, 799.99));
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
        var returncode = "Item added to cart.";
        var cart = _context.Carts.Find(cartID);
        var cartnew = deserializeItem(cart.cartSerial);

        
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

        return true;
    }

    public string GetTotals(string cartID)
    {
        var returncode = "Totals found.";
        //TODO: update to linq and manually create obj
        var cart = _context.Carts.Find(cartID);
        var cartnew = deserializeItem(cart.cartSerial);
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
        //TODO: Add bundle logic
        bundleTotal = runningTotal;
        var bundleCheck = cartnew.items;
        
            for (int i = 0; i < bundleCollection.Count; i++)
            {
                var passcheck = true;
                var total = 0.0;
                var diff = 0.0;
                for (int j = 0; j < bundleCollection[i].items.Count; j++)
                {
                    total += _context.Items.Find(bundleCollection[i].items[j]).price;
                    if (!bundleCheck.Contains(bundleCollection[i].items[j]))
                    {
                        passcheck = false;
                        break;
                    }
                }
                if (passcheck)
                {
                    // Calculate difference from prices of items in bundle and bundle price
                    diff = total -= bundleCollection[i].price;
                    diff = Math.Round(diff, 2);
                    bundleTotal -= diff;
                }
            }
        totalTax = bundleTotal * 0.07;
        output = "Subtotal: " + Math.Round(bundleTotal, 2, MidpointRounding.AwayFromZero) + "\nTax: " + Math.Round(totalTax, 2, MidpointRounding.AwayFromZero) + "\nTotal: " + Math.Round((bundleTotal + totalTax), 2, MidpointRounding.AwayFromZero);
        return output;
    }

    public Cart RemoveItem(string cartID, string itemID)
    {
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
}