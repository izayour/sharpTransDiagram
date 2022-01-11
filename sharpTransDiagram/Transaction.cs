using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Domain.Models
{
    public partial class Transaction
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; } = new DateTime();
        public virtual bool Direction { get; set; } = true;
        public double Quantity { get; set; } = 0;

        [NotMapped]
        //public Gateway Gateway { get; set; }
        public int TargetId { get; set; }
        [MaxLength(20)]
        public string Unit { get; set; }
        public string TargetType { get; set; }
        public string TargetAttribute { get; set; }

    }
    public partial class Transaction
    {
        public DummyData theDummy { get; set; }
        public Transaction(string TargetType, string TargetAttribute)
        {
            this.TargetType = TargetType;
            this.TargetAttribute = TargetAttribute;
        }

        public virtual bool Post()
        {
            double qty;
            if (!Direction)
                Quantity = -Quantity;
            update(Quantity, TargetType, TargetAttribute, TargetId);

            return true;

        }

        private void update(double quantity, string targetType, string targetAttribute, int targetId)
        {
            var targetList = theDummy.GetList<Target>(targetType);

            int index = targetList.FindIndex(t => t.GetTargetId() == targetId);
            targetList[index].Update(quantity, targetAttribute);

        }


        public void UnPost()
        {
            if (Direction)//true for income 
                Quantity = -Quantity;
            //this.Gateway.GetTargetObjectAndUpdate(TargetId, Quantity);

        }

    }
}
