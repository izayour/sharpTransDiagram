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

        public DateTime Date { get; set; } = new();
        public bool Direction { get; set; }
        public int TargetId { get; set; }
        public List<ItemEntry> Entries { get; set; }
        public List<Transaction> LeafTransactions { get; set; } = new();

        [NotMapped]
        public Double Total { get; set; } = 0;

        public List<Transaction> LeafTransList = new();
        public DummyData TheDummy { get; set; }
    }

    public partial class CompoundTransaction
    {
        public CompoundTransaction()
        {
        }

        public virtual bool Post()
        {
            this.LeafTransList.ForEach(lt => lt.Post());
            return true;
        }

        public virtual bool UnPost()
        {
            this.LeafTransList.ForEach(lt => lt.UnPost());
            return true;
        }

        public double ReturnTotalPost()
        {
            return Total;
        }
    }
}