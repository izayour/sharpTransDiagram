using sharpTransDiagram;
using System;
using System.Collections.Generic;
using sharpTransDiagram.Models;

namespace sharpTransDiagram.Common
{
    public class DummyData
    {
        public List<Target> Customers { get; set; } = new List<Target>
        {
            new Customer{Id=1}
        };

        public List<Target> Vendors { get; set; } = new List<Target>
        {
            new Vendor{Id=1}
        };

        public List<Target> Items { get; set; } = new List<Target>()
        {
            new Item{Id=1,HubId=1,OnHand=0,OnPO=0},
            new Item{Id=2,HubId=1,OnHand=0,OnPO=0}
        };

        public List<ItemEntry> ItemEntries { get; set; } = new List<ItemEntry>()
        {
            new ItemEntry{Id=1, Qty=1,Price=10},
            new ItemEntry{Id=2,Qty=2,Price=20}
        };

        public List<T> GetList<T>(string listName)
        {
            var prop = this.GetType().GetProperty(listName);
            if (prop != null)
            {
                return (List<T>)prop.GetValue(this);
            }
            else
            {
                throw new Exception("Data Not Found ");
            }
        }
    }
}