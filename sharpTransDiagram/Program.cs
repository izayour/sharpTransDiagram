using sharpTransDiagram;
using System;
using sharpTransDiagram.Models;
using sharpTransDiagram.Models.CompundTransactions;
using sharpTransDiagram.Common;

namespace sharpTransDiagram
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            DummyData myDumy = new();

            Item myItem = new Item { Id = 1, HubId = 1, OnHand = 0, OnPO = 0 };
            myDumy.Items.Add(myItem);

            PO myPO = new PO();
            myPO.Date = DateTime.Now;
            myPO.Id = 1;
            myPO.TargetId = 1;  //vendor
            myPO.HubId = 1;
            myPO.TheDummy = myDumy;
            myPO.CreateTransForItem(myItem.Id, 2, 10);
            myPO.CreateAccountTransaction();

            SO mySO = new SO();
            mySO.Date = DateTime.Now;
            mySO.Id = 1;
            mySO.TargetId = 1;
            mySO.HubId = 1;
            mySO.TheDummy = myDumy;
            mySO.CreateTransForItem(myItem.Id, 2, 10);
            mySO.CreateAccountTransaction();
            Console.WriteLine("Posting PO\n");
            myPO.Post();
            Console.WriteLine("UnPosting PO\n");

            myPO.UnPost();

            //System.Diagnostics.Debug.Assert(myItem.OnPO == 2);

            Console.WriteLine("****************************************************************\n");
            Console.WriteLine("Posting SO\n");

            mySO.Post();
            Console.WriteLine("UnPosting SO\n");

            mySO.UnPost();
        }
    }
}