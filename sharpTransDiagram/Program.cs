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
            DummyData  myDumy = new DummyData();
            Item myItem = new Item { Id = 1, OnHand = 0, OnPO = 0 };
            myDumy.Items.Add(myItem);

            PO myPO = new PO();
            myPO.Date = DateTime.Now;
            myPO.Id = 1;
            myPO.ChildrenGateway.theDummy = myDumy;
            StockHubTrans sht1 = new StockHubTrans
            { Id = 1, Direction = true, Gateway = myPO.ChildrenGateway, TargetId = 1, Quantity = 2, Price = 2 };


            myPO.leafTransList.Add(sht1);
            
            myPO.Post();
            System.Diagnostics.Debug.Assert(myItem.OnPO == 2);



       }
    }
}
