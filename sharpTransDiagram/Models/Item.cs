using System;
using System.ComponentModel.DataAnnotations;

namespace sharpTransDiagram.Models
{
    public partial class Item : Target
    {
        [Key]
        public int Id { get; set; }

        public int HubId { get; set; }
        public Hub Hub { get; set; }
        public int OnHand { get; set; } = 0;
        public int OnPO { get; set; } = 0;
        public int OnSO { get; set; } = 0;
    }

    public partial class Item : Target
    {
        public override int GetTargetId()
        {
            return this.Id;
        }

        public override void Update(double quantity, string attribute)
        {
            var prop = this.GetType().GetProperty(attribute);
            if (prop != null)
            {
                int value = (int)prop.GetValue(this);

                this.GetType().GetProperty(attribute).SetValue(this, value + (int)quantity);
                Console.WriteLine("\tItem (" + Id + ") : " + this.GetType().GetProperty(attribute).Name + " updated " + value + " -> " + this.GetType().GetProperty(attribute).GetValue(this).ToString() + "\n");
            }
            else
            {
                throw new Exception("properity " + attribute + " not found in class" + this.GetType().Name);
            }
        }
    }
}