using Accounts.Domain.Entities;
using Accounts.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Domain.Abstraction.Services
{
    public interface IUserServices
    {
        
        Task RegisterUser(string username, string password);
        Task<bool> AuthenticateUser(string username, string password);
        Task<Users> GetUserByUsername(string username);
        Task<Users> GetByIdAsync(int userId);


    }


}
