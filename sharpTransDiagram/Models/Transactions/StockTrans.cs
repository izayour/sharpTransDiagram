using sharpTransDiagram.Common;

namespace sharpTransDiagram.Models.Transactions
{
    public class StockTrans : Transaction
    {
        public double Price { get; set; }

        public StockTrans(string Attribute) : base(Constants.Item, Attribute)
        {
        }

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