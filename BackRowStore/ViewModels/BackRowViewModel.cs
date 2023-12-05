using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackRowStore.ViewModels
{

    [Table("Items")]
    public class BackRowViewModel
    {
        public string itemID { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
    }

    [Table("Carts")]
    public class Cart
    {
        public string cartId { get; set; }

        public string items { get; set; }

        public double cartBalance { get; set; }
    }


}
