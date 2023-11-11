﻿using Accounts.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Domain.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
       
        Task<decimal> GetBalanceAsync(int UserID);
    }
}
