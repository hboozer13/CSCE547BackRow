using System;

namespace BackRowStore 
{
    public class Checkout
    {
        //public Cart currentCart = new Cart();
        public int cardNumber { get; set; }
        public DateTime exp {  get; set; }    
        public string cardholderName { get; set; }
        public int cvc { get; set; }

        public Checkout()
        {
        }
    }
}


// THIS CLASS DOESNT DO FUCKING ANYTHING

