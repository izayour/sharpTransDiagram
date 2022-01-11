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
            DummyData myDumy = new();

            Item myItem = new Item { Id = 1, HubId = 1, OnHand = 0, OnPO = 0 };
            myDumy.Items.Add(myItem);

            PO myPO = new PO();
            myPO.Date = DateTime.Now;
            myPO.Id = 1;
            myPO.TargetId = 1;
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

            myPO.Post();
            myPO.UnPost();

            //System.Diagnostics.Debug.Assert(myItem.OnPO == 2);

            Console.WriteLine("****************************************************************\n");

            mySO.Post();
            mySO.UnPost();
        }
    }
}