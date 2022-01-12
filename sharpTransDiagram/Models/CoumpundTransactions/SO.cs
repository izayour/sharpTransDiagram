using sharpTransDiagram.Common;
using sharpTransDiagram.Models;
using sharpTransDiagram.Models.Transactions;
using System.Collections.Generic;

namespace sharpTransDiagram
{
    public class SO : CompoundTransaction
    {
        public int HubId { get; set; }

        public SO(DummyData theDummy) : base(theDummy)
        {
        }

        public void CreateTransForItem(List<ItemEntry> itemEntries)
        {
            itemEntries.ForEach(e =>
            {
                StockHubTrans sht1 = new StockHubTrans(Constants.OnSo)
                { Id = 1, HubId = this.HubId, Direction = true, TargetId = e.ItemId, Quantity = e.Qty, Price = e.Price, TheDummy = this.TheDummy };

                this.Total += sht1.GetAmount();

                this.LeafTransList.Add(sht1);
            });
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