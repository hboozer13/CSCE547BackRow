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
        public IActionResult ProcessPayment(string cartId, string cardNumber, DateTime exp, string cardholderName, string cvc)
        {
            if (cardNumber.Length == 16 && cvc.Length == 3 && exp.Date > DateTime.Now.Date && _dataService.cartExists(cartId)) {
                /*foreach (string item in _dataService.getCart(cartId))
                {
                    var itemKey = _dataService.getShop()[item];
                    _dataService.getShop()[item] = (itemKey.Item1, itemKey.Item2, (itemKey.Item3 - 1));
                }*/
                return Accepted();
            } else
            {
                return BadRequest();
            }
                
        }
    }
}

