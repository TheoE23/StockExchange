using Accounts.Domain.Abstraction.Services;
using Accounts.Domain.Entities;
using Accounts.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Domain.Services
{
    public class UserService : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashing _passwordHasher;

        public UserService(IUserRepository userRepository, IPasswordHashing passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task RegisterUser(string UserName, string UserPassword)
        {
            string hashedPassword = _passwordHasher.HashPassword(UserPassword);
        
            Users User = new Users { UserName = UserName, UserPassword = hashedPassword };

            await _userRepository.CreateAsync(User);
        }

        public async Task<bool> AuthenticateUser(string UserName, string UserPassword)
        {
            Users user = await _userRepository.GetByUsername(UserName);

            bool passwordIsValid = _passwordHasher.VerifyPassword(UserPassword, user.UserPassword);

            return passwordIsValid;
        }
        public async Task AddUser(Users user)
        {
            await _userRepository.CreateAsync(user);
        }

        public async Task<Users> GetUserByUsername(string username)
        {
            return await _userRepository.GetByUsername(username);
        }

       
    }

}
