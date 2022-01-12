namespace sharpTransDiagram.Models.Transactions
{
    public class StockHubTrans : StockTrans
    {
        public int HubId { get; set; }

        public StockHubTrans(string StockAttribute) : base(StockAttribute)
        {
        }
    }
}