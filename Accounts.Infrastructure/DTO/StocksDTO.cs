using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Infrastructure.DTO
{
    public class StocksDTO
    {
        public int StockID { get; set; }
        public string? StockName { get; set; }
        public decimal CurrentPrice { get; set; }
        public int Quantity { get; set; }
      
        public string? Currency { get; set; }
    }
}
