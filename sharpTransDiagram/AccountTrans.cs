using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Domain.Models.Transactions
{
    public class AccountTrans : Transaction
    {
        public AccountTrans(string AccountType, string AccountAttribute) : base(AccountType, AccountAttribute)
        {

        }
    }
}
