using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Domain.Models
{
    public partial class CompoundTransaction
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; } = new DateTime();
        public bool Direction { get; set; }
        public int TargetId { get; set; }
        public List<ItemEntry> Entries { get; set; }
        public List<Transaction> LeafTransactions { get; set; } = new List<Transaction>();

        [NotMapped]
        public Double Total { get; set; } = 0;

        public List<Transaction> leafTransList = new List<Transaction>();
        public DummyData theDummy { get; set; }
    }

    public partial class CompoundTransaction
    {
        public CompoundTransaction()
        {
        }

        public virtual bool Post()
        {
            this.leafTransList.ForEach(lt => lt.Post());
            return true;
        }

        public double ReturnTotalPost()
        {
            return Total;
        }
    }
}