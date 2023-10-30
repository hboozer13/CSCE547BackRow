﻿using BackRowStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackRowStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckoutController : ControllerBase
    {
        private readonly ILogger<CheckoutController> _logger;
        private readonly IDataService _dataService;


        public CheckoutController(IDataService DataService)
        {
            _dataService = DataService;
        }
        /*
        public CheckoutController(ILogger<CheckoutController> logger)
        {
            _logger = logger;
        }
        */

        [HttpGet("ProcessPayment", Name = "ProcessPayment")]
        public IEnumerable<Item> ProcessPayment()
        {
            return Enumerable.Range(1, 5).Select(index => new Item
            {
                name = "ITEM",
            })
            .ToArray();
        }
    }
}

