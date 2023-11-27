using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Domain.Interfaces
{
    public interface IPasswordHashing
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }

}
