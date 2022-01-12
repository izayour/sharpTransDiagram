using System;
using sharpTransDiagram.Models;
using sharpTransDiagram.Models.CompundTransactions;
using sharpTransDiagram.Common;
using System.Collections.Generic;

namespace sharpTransDiagram
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            DummyData myDummy = new();
            List<ItemEntry> itemEntries = new();
            itemEntries.Add(new() { Id = 1, ItemId = 1, Price = 10, Qty = 1 });
            itemEntries.Add(new() { Id = 2, ItemId = 2, Price = 20, Qty = 1 });

            PO myPO = new PO(myDummy)
            {
                Id = 1,
                Date = DateTime.Now,
                TargetId = 1,
                HubId = 1
            };

            myPO.CreateTransForItem(itemEntries);
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
            mySO.CreateTransForItem(itemEntries);
            mySO.CreateAccountTransaction();

            Console.WriteLine("Posting SO\n");

            mySO.Post();

            Console.WriteLine("UnPosting SO\n");

            mySO.UnPost();
        }
    }
}