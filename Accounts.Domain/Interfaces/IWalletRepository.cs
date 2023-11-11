using Accounts.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Domain.Interfaces
{
    public interface IWalletRepository : IRepository<Wallets>
    {
        Task<decimal> GetTotalPurchasePriceForWalletAsync(int WalletID);
        
    }

}
