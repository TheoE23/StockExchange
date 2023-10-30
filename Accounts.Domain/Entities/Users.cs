using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Domain.Entities
{
    public class Users
    {
        public int UserID { get;  set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get;  set; }
        public DateTime Birthday { get;  set; }
    }
}
