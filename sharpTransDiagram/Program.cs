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
            List<ItemOrder> itemEntries = new();
            itemEntries.Add(new() { ItemHubId = 1, Qty = 2, Price = 10 }); // 3 available on hand
            itemEntries.Add(new() { ItemHubId = 2, Qty = 3, Price = 20 }); // 1 available on hand

            SO mySO = new SO(myDummy)
            {
                Id = 1,
                Date = DateTime.Now,
                TargetId = 1,
                HubId = 1,
                ItemOrders = itemEntries,
                ShippingMethod = ShippingMethods.ShippingMethod1,
                ShippingInfoId = 1
            };
            mySO.CreateTransForItem();
            mySO.CreateAccountTransaction();

            SO mySO2 = new SO(myDummy)
            {
                Id = 1,
                Date = DateTime.Now,
                TargetId = 1,
                HubId = 1,
                ItemOrders = itemEntries
            };

            Console.WriteLine("Posting/Creating SO\n");

            mySO.Post();
            Console.WriteLine("SO Status: " + mySO.GetState() + "\n");

            Console.WriteLine("****************************************************************\n");

            mySO.Fulfill(mySO.ItemOrders[0], 1);
            mySO.Fulfill(mySO.ItemOrders[0], 2);
            mySO.Fulfill(mySO.ItemOrders[1], 1);

            myDummy.Serials.ForEach(s => Console.WriteLine("SerialNo: " + s.SerialNo + ", Available: " + s.IsAvaialable)
            );
            Console.WriteLine("\ntotal: " + mySO.Total + " Fulfilled: " + mySO.FulfillTotal);
            Console.WriteLine("\nSO Status: " + mySO.GetState() + "\n");

            //Console.WriteLine("creating So2");
            //mySO2.CreateTransForItem();
            //mySO2.CreateAccountTransaction();
            //mySO2.Post();

            //mySO.Void();
            Console.WriteLine("****************************************************************\n");

            mySO.ShipPickUp();
            Console.WriteLine("SO Status: " + mySO.GetState() + "\n");

            //Console.WriteLine("****************************************************************\n");
            //mySO2.FulfillAll();
            //myDummy.Serials.ForEach(s => Console.WriteLine("SerialNo: " + s.SerialNo + ", Available: " + s.IsAvaialable)
            //);
            //Console.WriteLine("total: " + mySO2.Total + " Fulfilled: " + mySO2.FulfillTotal);
            //Console.WriteLine("SO Status: " + mySO2.GetState() + "\n");
            //mySO2.ShipPickUp();
        }
    }
}