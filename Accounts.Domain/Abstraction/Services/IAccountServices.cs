using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Domain.Abstraction.Services
{
    public interface IAccountServices
    {
        Task FundAccountAsync(int UserID, decimal amount, string currency);
        Task PurchaseStockAsync(int UserID, string stockName, int quantity, decimal currentPrice);
    }
}
