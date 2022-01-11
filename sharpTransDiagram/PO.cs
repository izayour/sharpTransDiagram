using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Domain.Models.Gateways;
using WebApp.Domain.Models.Transactions;


namespace WebApp.Domain.Models.CompundTransactions
{
    public class PO : CompoundTransaction
    {
        public int HubId { get; set; }

        public PO()
        {


        }
        public void createTransForItem(int itemId, int customerId, int Qty, double price)
        {
            StockHubTrans sht1 = new StockHubTrans("OnPO")
            { Id = 1, Direction = true, TargetId = itemId, Quantity = Qty, Price = price, theDummy = this.theDummy };
            StockHubTrans sht2 = new StockHubTrans("OnHand")
            { Id = 2, Direction = false, TargetId = itemId, Quantity = Qty, Price = price, theDummy = this.theDummy };
            this.Total += sht1.GetAmount();
            AccountTrans act1 = new AccountTrans("Customers", "OnPO") { Id = 1, Direction = true, TargetId = customerId, Quantity = this.Total, theDummy = this.theDummy };
            this.leafTransList.Add(sht1);
            this.leafTransList.Add(sht2);
            this.leafTransList.Add(act1);
        }
        public override bool Post()
        {
            this.leafTransList.ForEach(aLeaf => aLeaf.Post());
            return true;
        }
    }
}
