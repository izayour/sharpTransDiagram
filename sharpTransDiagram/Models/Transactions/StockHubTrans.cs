namespace sharpTransDiagram.Models.Transactions
{
    public class StockHubTrans : StockTrans
    {
        public int HubId { get; set; }

        public Hub Hub
        {
            get => default;
            set
            {
            }
        }

        public StockHubTrans(string StockAttribute) : base(StockAttribute)
        {
        }
    }
}