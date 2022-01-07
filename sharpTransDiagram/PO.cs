using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Domain.Models.Gateways;
using WebApp.Domain.Models.Transactions;


namespace WebApp.Domain.Models.CompundTransactions
{
    public class PO : CompoundTransaction
    {
        public int HubId { get; set; }

        public PO()
        {


        }
        public override bool Post()
        {
            this.leafTransList.ForEach(aLeaf => aLeaf.Post());
            return true;
        }
    }
}
