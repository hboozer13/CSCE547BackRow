using System;
using System.Linq;
using BackRowStore;
using Newtonsoft.Json;
public class CheckoutService
{
    private readonly BackRowDbContext _context;

    public CheckoutService(BackRowDbContext context)
    {
        _context = context;
    }
}
