using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharpTransDiagram.Models
{
    public class ItemHub : Target
    {
        public int Id { get; set; }
        public bool IsSerialized { get; set; }
        public int ItemId { get; set; }
        public int HubId { get; set; }
        public int OnHand { get; set; } = 0;
        public int OnPO { get; set; } = 0;
        public int OnSO { get; set; } = 0;
        public int OnFulfill { get; set; } = 0;

        public ItemHub(bool IsSerialized)
        {
            this.IsSerialized = IsSerialized;
        }

        public void Ship(int qty)
        {
            this.OnHand -= qty;
            this.OnSO -= qty;
            this.OnFulfill -= qty;
        }

        public override int GetTargetId()
        {
            return this.Id;
        }

        public override void Update(double quantity, string attribute)
        {
            var prop = this.GetType().GetProperty(attribute);
            this.GetType().GetMethod("GetTargetId").Invoke(this, null);
            if (prop != null)
            {
                int value = (int)prop.GetValue(this);

                this.GetType().GetProperty(attribute).SetValue(this, value + (int)quantity);
                Console.WriteLine("\tItem (" + ItemId + ") on Hub(" + HubId + ") " + " : " + this.GetType().GetProperty(attribute).Name + " updated " + value + " -> " + this.GetType().GetProperty(attribute).GetValue(this).ToString() + "\n");
            }
            else
            {
                throw new Exception("properity " + attribute + " not found in class" + this.GetType().Name);
            }
        }
    }
}