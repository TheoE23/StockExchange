﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Infrastructure.DTO_s
{
    internal class transactions
    {
        public int TransactionID { get; set; }
        public int UserID { get; set; }
        public string? UserName { get; set; }
        public int StockId { get; set; }
        public string? StockName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string? TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
