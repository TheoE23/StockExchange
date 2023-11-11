using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Infrastructure.DTO
{
    public class UsersDTO
    {
        public int UserID { get; set; }
        public string? UserName { get; set; }
        public string? UserPassword { get; set; }
        public string? Email { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
