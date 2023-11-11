using Accounts.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace Accounts.Domain.Interfaces
{
    public interface ITransactionRepository :IRepository<Transactions>
    {
        IEnumerable<Transactions> GetTransactionsByType(string TransactionType);
    }

}
