using sharpTransDiagram.Common;
using sharpTransDiagram.Models;
using sharpTransDiagram.Models.Transactions;

namespace sharpTransDiagram
{
    public class SO : CompoundTransaction
    {
        public int HubId { get; set; }

        public void CreateTransForItem(int itemId, int Qty, double price)
        {
            StockHubTrans sht1 = new StockHubTrans(Constants.OnSo)
            { Id = 1, HubId = this.HubId, Direction = true, TargetId = itemId, Quantity = Qty, Price = price, TheDummy = this.TheDummy };
            //StockHubTrans sht2 = new StockHubTrans(Constants.OnHand)
            //{ Id = 2, Direction = true, HubId = this.HubId, TargetId = itemId, Quantity = Qty, Price = price, TheDummy = this.TheDummy };
            this.Total += sht1.GetAmount();

            this.LeafTransList.Add(sht1);
            //this.leafTransList.Add(sht2);
        }

        public void CreateAccountTransaction()
        {
            AccountTrans act1 = new AccountTrans(Constants.Customer, Constants.OnSo) { Id = 1, Direction = true, TargetId = this.TargetId, Quantity = this.Total, TheDummy = this.TheDummy };
            LeafTransList.Add(act1);
        }

        //public void Ship()
        //{
        //}
    }
}