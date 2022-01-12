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
            DummyData myDummy = new();

            Item myItem = new Item { Id = 1, HubId = 1, OnHand = 0, OnPO = 0 };
            myDummy.Items.Add(myItem);

            PO myPO = new PO(myDummy)
            {
                Id = 1,
                Date = DateTime.Now,
                TargetId = 1,
                HubId = 1
            };

            myPO.CreateTransForItem(myItem.Id, 2, 10);
            myPO.CreateAccountTransaction();

            Console.WriteLine("Posting PO\n");

            myPO.Post();

            Console.WriteLine("UnPosting PO\n");

            myPO.UnPost();

            Console.WriteLine("****************************************************************\n");

            SO mySO = new SO(myDummy)
            {
                Id = 1,
                Date = DateTime.Now,
                TargetId = 1,
                HubId = 1
            };
            mySO.CreateTransForItem(myItem.Id, 2, 10);
            mySO.CreateAccountTransaction();

            Console.WriteLine("Posting SO\n");

            mySO.Post();

            Console.WriteLine("UnPosting SO\n");

            mySO.UnPost();
        }
    }
}