using sharpTransDiagram;
using System.Collections.Generic;
using sharpTransDiagram.Models.Transactions;
using sharpTransDiagram.Common;

namespace sharpTransDiagram.Models.CompundTransactions
{
    public class PO : CompoundTransaction
    {
        public int HubId { get; set; }

        public void CreateTransForItem(int itemId, int Qty, double price)
        {
            StockHubTrans sht1 = new StockHubTrans(Constants.OnPo)
            { Id = 1, HubId = this.HubId, Direction = true, TargetId = itemId, Quantity = Qty, Price = price, TheDummy = this.TheDummy };
            //StockHubTrans sht2 = new StockHubTrans(Constants.OnHand)
            //{ Id = 2, Direction = false, HubId = this.HubId, TargetId = itemId, Quantity = Qty, Price = price, TheDummy = this.TheDummy };
            this.Total += sht1.GetAmount();

            this.LeafTransList.Add(sht1);
            //this.leafTransList.Add(sht2);
        }

        public PO(DummyData theDummy) : base(theDummy)
        {
        }

        public void CreateAccountTransaction()
        {
            AccountTrans act1 = new AccountTrans(Constants.Vendor, Constants.OnPo) { Id = 1, Direction = true, TargetId = this.TargetId, Quantity = this.Total, TheDummy = this.TheDummy };
            LeafTransList.Add(act1);
        }

        //public void Approve()
        //{
        //    this.LeafTransList.ForEach(trans => trans.UnPost());
        //    StockHubTrans sht1 = new StockHubTrans(Constants.OnHand)
        //    { };
        //    AccountTrans act1 = new AccountTrans(Constants.Vendor, Constants.Balance) { Id = 1, Direction = true, TargetId = this.TargetId, Quantity = this.Total, TheDummy = this.TheDummy };
        //    LeafTransList.Add(act1);
        //}
    }
}