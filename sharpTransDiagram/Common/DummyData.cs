using sharpTransDiagram;
using System;
using System.Collections.Generic;
using sharpTransDiagram.Models;

namespace sharpTransDiagram.Common
{
    public class DummyData
    {
        public List<Target> Vendors { get; set; } = new List<Target>
        {
            new Vendor{Id=1}
        };

        public List<Target> Customers { get; set; } = new List<Target>
        {
            new Customer{Id=1,OnSO=0,Balance=0}
        };

        public List<Item> Items { get; set; } = new List<Item>()
        {
            new Item{Id=1},
            new Item{Id=2}
        };

        public List<Serial> Serials = new List<Serial>()
        {
            new Serial{ItemId=1,SerialNo=1,IsAvaialable=true},
            new Serial{ItemId=1,SerialNo=2,IsAvaialable=true},
            new Serial{ItemId=1,SerialNo=3,IsAvaialable=true},
        };

        public List<Target> ItemHubs { get; set; } = new List<Target>()
        {
            new ItemHub(true){Id=1,HubId=1,ItemId=1,OnHand=3,OnPO=0,OnSO=0},
            new ItemHub(false){Id=2,HubId=1,ItemId=2,OnHand=1,OnPO=0,OnSO=0}
        };

        public List<ItemOrder> ItemEntries { get; set; } = new List<ItemOrder>()
        {
            new ItemOrder{ Qty=1,Price=10},
            new ItemOrder{Qty=2,Price=20}
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

        public bool UpdateSerial(int itemId, int serialNo)
        {
            int index = this.Serials.FindIndex(s => s.ItemId == itemId && s.SerialNo == serialNo);
            if (index < 0)
            {
                return false;
            }
            if (!Serials[index].IsAvaialable)
            {
                return false;
            }
            Serials[index].IsAvaialable = false;
            return true;
        }
    }
}