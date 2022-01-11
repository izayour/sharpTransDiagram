namespace WebApp.Domain.Models.Transactions
{
    public class StockTrans : Transaction
    {
        public StockTrans(string Attribute) : base("Items", Attribute)
        {
        }

        public double Price { get; set; }

        public double GetAmount()
        {
            return this.Quantity * Price;
        }

        public double GetQty()
        {
            return this.Quantity;
        }
    }
}