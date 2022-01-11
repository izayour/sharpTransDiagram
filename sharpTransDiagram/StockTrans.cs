using sharpTransDiagram;

namespace WebApp.Domain.Models.Transactions
{
    public class StockTrans : Transaction
    {
        public StockTrans(string Attribute) : base(Constants.Item, Attribute)
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