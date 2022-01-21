using sharpTransDiagram;
using System.Collections.Generic;
using sharpTransDiagram.Models.Transactions;
using sharpTransDiagram.Common;

namespace sharpTransDiagram.Models.CompundTransactions
{
    public class PO : CompoundTransaction
    {
        public PO(DummyData theDummy) : base(theDummy)
        {
        }

        public void CreateTransForItem(List<ItemOrder> itemEntries)
        {
            itemEntries.ForEach(e =>
            {
                StockTrans sht1 = new StockTrans(Constants.OnPo)
                { Id = 1, Adding = true, TargetId = e.ItemHubId, Quantity = e.Qty, Price = e.Price, TheDummy = this.TheDummy };
                this.Total += sht1.GetAmount();

                this.LeafTransList.Add(sht1);
            });
        }

        public void CreateAccountTransaction()
        {
            AccountTrans act1 = new AccountTrans(Constants.Vendor, Constants.OnPo) { Id = 1, Adding = true, TargetId = this.TargetId, Quantity = this.Total, TheDummy = this.TheDummy };
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