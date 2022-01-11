using System;
using System.Collections.Generic;
using WebApp.Domain.Models;

namespace ConsoleApp1
{
    public class DummyData
    {
        public List<Target> Customers { get; set; } = new List<Target>
        {
            new Customer{Id=1,OnSO=0,OnPO=0, }
        };

        public List<Target> Items { get; set; } = new List<Target>()
        {
            new Item{Id=5,OnHand=10,OnPO=10},
            new Item{Id=2,OnHand=20,OnPO=20},
            new Item{Id=3,OnHand=30,OnPO=30}
        };

        public List<ItemEntry> ItemEntries { get; set; } = new List<ItemEntry>()
        {
            new ItemEntry{Id=1, Qty=1,Price=10},
            new ItemEntry{Id=2,Qty=2,Price=20},
            new ItemEntry{Id=3,Qty=3,Price=30}
        };

        public List<T> GetList<T>(string listName)
        {
            try
            {
                return (List<T>)this.GetType().GetProperty(listName).GetValue(this);
            }
            catch (Exception)
            {
                throw new Exception("Data Not Found ");
            }
        }
    }
}