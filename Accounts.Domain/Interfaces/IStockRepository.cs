using Accounts.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Domain.Interfaces
{
    public interface IStockRepository : IRepository<Stocks>
    {
        Task<Stocks> GetStockByNameAsync(string StockName);
       
    }
}
