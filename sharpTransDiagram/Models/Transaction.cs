using sharpTransDiagram.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sharpTransDiagram.Models
{
    public partial class Transaction
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; } = new DateTime();
        public virtual bool Direction { get; set; } = true;
        public double Quantity { get; set; } = 0;
        public int TargetId { get; set; }
        public string Unit { get; set; }
        public string TargetType { get; set; }
        public string TargetAttribute { get; set; }

        public Target Target
        {
            get => default;
            set
            {
            }
        }
    }

    public partial class Transaction
    {
        public DummyData TheDummy { get; set; }

        public Transaction(string TargetType, string TargetAttribute)
        {
            this.TargetType = TargetType;
            this.TargetAttribute = TargetAttribute;
        }

        public virtual void Post()
        {
            if (Direction)
            {
                UpdateTarget(Quantity, TargetType, TargetAttribute, TargetId);
            }
            else
            {
                UpdateTarget(-Quantity, TargetType, TargetAttribute, TargetId);
            }
        }

        private void UpdateTarget(double quantity, string targetType, string targetAttribute, int targetId)
        {
            var targetList = TheDummy.GetList<Target>(targetType);

            int index = targetList.FindIndex(t => t.GetTargetId() == targetId);
            targetList[index].Update(quantity, targetAttribute);
        }

        public void UnPost()
        {
            if (!Direction)
            {
                UpdateTarget(Quantity, TargetType, TargetAttribute, TargetId);
            }
            else
            {
                UpdateTarget(-Quantity, TargetType, TargetAttribute, TargetId);
            }
        }
    }
}