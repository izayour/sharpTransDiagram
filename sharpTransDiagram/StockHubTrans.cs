namespace WebApp.Domain.Models.Transactions
{
    public class StockHubTrans : StockTrans
    {
        public int HubId { get; set; }

        public StockHubTrans(string StockAttribute) : base(StockAttribute)
        {
        }
    }
}