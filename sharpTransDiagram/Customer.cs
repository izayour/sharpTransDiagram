using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Domain.Models
{
    public partial class Customer : Target
    {
        [Key]
        public int Id { get; set; }

        public double OnSO { get; set; } = 0;
        public double Balance { get; set; } = 0;
    }

    public partial class Customer : Target
    {
        public override int GetTargetId()
        {
            return this.Id;
        }

        internal override bool Update(double quantity, string attribute)
        {
            if (string.IsNullOrWhiteSpace(attribute))
            {
                throw new ArgumentException($"'{nameof(attribute)}' cannot be null or whitespace.", nameof(attribute));
            }

            if (this.GetType().GetProperty(attribute) != null)
            {
                double value = (double)this.GetType().GetProperty(attribute).GetValue(this);

                this.GetType().GetProperty(attribute).SetValue(this, value + quantity);

                Console.WriteLine("Customer (" + Id + ") : " + this.GetType().GetProperty(attribute).Name + " updated " + value + " -> " + this.GetType().GetProperty(attribute).GetValue(this).ToString() + "\n");

                return true;
            }
            return false;
        }
    }
}