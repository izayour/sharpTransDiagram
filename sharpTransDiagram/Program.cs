using ConsoleApp1;
using System;
using WebApp.Domain.Models;
using WebApp.Domain.Models.CompundTransactions;
using WebApp.Domain.Models.Transactions;

namespace sharpTransDiagram
{
    class Program
    {
        static DummyData myDumy;
        static void Main(string[] args)
        {
            DummyData myDumy = new DummyData();

            Item myItem = new Item { Id = 1, OnHand = 0, OnPO = 0 };
            myDumy.Items.Add(myItem);

            PO myPO = new PO();
            myPO.Date = DateTime.Now;
            myPO.Id = 1;

            StockHubTrans sht1 = new StockHubTrans("OnPO")
            { Id = 1, Direction = true, TargetId = 1, Quantity = 2, Price = 2, theDummy = myDumy };
            myPO.Total += sht1.GetAmount();
            AccountTrans act1 = new AccountTrans("Customers", "OnPO") { Id = 1, Direction = true, TargetId = 1, Quantity = myPO.Total, theDummy = myDumy };

            myPO.leaf`List.Add(sht1);
            myPO.leafTransList.Add(act1);

            myPO.Post();
            System.Diagnostics.Debug.Assert(myItem.OnPO == 2);
        }
    }
}
