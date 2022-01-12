using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sharpTransDiagram.Models;

namespace sharpTransDiagram
{
    public partial class Vendor : Target
    {
        public int Id { get; set; }
        public double OnPO { get; set; } = 0;
        public double Balance { get; set; } = 0;
    }

    public partial class Vendor : Target
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
                double value = (double)prop.GetValue(this);

                this.GetType().GetProperty(attribute).SetValue(this, value + quantity);

                Console.WriteLine("\tVendor (" + Id + ") : " + this.GetType().GetProperty(attribute).Name + " updated " + value + " -> " + this.GetType().GetProperty(attribute).GetValue(this).ToString() + "\n");
            }
            else
            {
                throw new Exception("properity " + attribute + " not found in class" + this.GetType().Name);
            }
        }
    }
}