using sharpTransDiagram.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sharpTransDiagram.Models
{
    public partial class CompoundTransaction
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; } = new();
        public bool Direction { get; set; }
        public int TargetId { get; set; }
        public Double Total { get; set; } = 0;

        public List<Transaction> LeafTransList { get; set; } = new();
        public DummyData TheDummy { get; set; }

        public Transaction Transaction
        {
            get => default;
            set
            {
            }
        }
    }

    public partial class CompoundTransaction
    {
        public virtual void Post()
        {
            this.LeafTransList.ForEach(lt => lt.Post());
        }

        public virtual void UnPost()
        {
            this.LeafTransList.ForEach(lt => lt.UnPost());
        }

        public double ReturnTotalPost()
        {
            return Total;
        }
    }
}