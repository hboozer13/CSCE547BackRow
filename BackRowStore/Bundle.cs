namespace BackRowStore
{
    public class Bundle
    {
        public string bundleID { get; set; }
        public List<string> items { get; set; }

        public double price { get; set; }

        public Bundle(string bundleID, List<string> items, double price)
        {
            this.bundleID = bundleID;
            this.items = items;
            this.price = price;
        }
    }
}
