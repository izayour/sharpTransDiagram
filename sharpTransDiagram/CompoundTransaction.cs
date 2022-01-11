using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Domain.Models.Transactions;


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
        //public virtual Gateway CustomerGateway { get; set; }
        //[NotMapped]
        //public virtual Gateway ChildrenGateway { get; set; }
        //public String StockAttribute { get; set; }
        //public String AccountType { get; set; }
        //public String AccountAttribute { get; set; }
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
