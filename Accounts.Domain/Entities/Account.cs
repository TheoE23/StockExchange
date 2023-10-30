using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Domain.Entities
{
    public class Account
    {
        public int AccountID { get; set; }
        public int UserID { get; set; }
        public decimal AccountBalance { get;  set; }
        public string? Currency { get;  set; }
    }
}
