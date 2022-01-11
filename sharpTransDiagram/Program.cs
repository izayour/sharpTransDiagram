using ConsoleApp1;
using System;
using WebApp.Domain.Models;
using WebApp.Domain.Models.CompundTransactions;

namespace sharpTransDiagram
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            DummyData myDumy = new DummyData();

            Item myItem = new Item { Id = 1, OnHand = 0, OnPO = 0 };
            myDumy.Items.Add(myItem);

            PO myPO = new PO();
            myPO.Date = DateTime.Now;
            myPO.Id = 1;
            myPO.TargetId = 1;
            myPO.theDummy = myDumy;
            myPO.CreateTransForItem(myItem.Id, 2, 10);
            myPO.CreateAccountTransaction();

            myPO.Post();
            System.Diagnostics.Debug.Assert(myItem.OnPO == 2);
        }
    }
}