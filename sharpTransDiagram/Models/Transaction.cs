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
        public virtual bool Adding { get; set; } = true;
        public double Quantity { get; set; } = 0;
        public int TargetId { get; set; }
        public string Unit { get; set; }
        public string TargetType { get; set; }
        public string TargetAttribute { get; set; }
        public bool IsPosted { get; set; } = false;
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
            this.IsPosted = true;
            if (Adding)
            {
                UpdateTarget(Quantity, TargetType, TargetAttribute, TargetId);
            }
            else
            {
                UpdateTarget(-Quantity, TargetType, TargetAttribute, TargetId);
            }
        }

        public virtual void UpdateTarget(double quantity, string targetType, string targetAttribute, int targetId)
        {
            this.IsPosted = false;
            var targetList = TheDummy.GetList<Target>(targetType);

            int index = targetList.FindIndex(t => t.GetTargetId() == targetId);
            targetList[index].Update(quantity, targetAttribute);
        }

        public void UnPost()
        {
            if (!Adding)
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