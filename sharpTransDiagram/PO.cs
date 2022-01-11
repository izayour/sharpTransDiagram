using sharpTransDiagram;
using WebApp.Domain.Models.Transactions;

namespace WebApp.Domain.Models.CompundTransactions
{
    public class PO : CompoundTransaction
    {
        public int HubId { get; set; }

        public void CreateTransForItem(int itemId, int Qty, double price)
        {
            StockHubTrans sht1 = new StockHubTrans(Constants.OnPo)
            { Id = 1, HubId = this.HubId, Direction = true, TargetId = itemId, Quantity = Qty, Price = price, theDummy = this.theDummy };
            StockHubTrans sht2 = new StockHubTrans(Constants.OnHand)
            { Id = 2, Direction = false, HubId = this.HubId, TargetId = itemId, Quantity = Qty, Price = price, theDummy = this.theDummy };
            this.Total += sht1.GetAmount();

            this.leafTransList.Add(sht1);
            this.leafTransList.Add(sht2);
        }

        public void CreateAccountTransaction()
        {
            AccountTrans act1 = new AccountTrans(Constants.Customer, Constants.OnPo) { Id = 1, Direction = true, TargetId = this.TargetId, Quantity = this.Total, theDummy = this.theDummy };
            leafTransList.Add(act1);
        }

        public override bool Post()
        {
            this.leafTransList.ForEach(aLeaf => aLeaf.Post());
            return true;
        }
    }
}