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

    [Table("CartSerial")]
    public class CartSerial
    {
        public string cartSerial { get; set; }
    }


}
