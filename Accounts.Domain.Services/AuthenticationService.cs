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
    public class AuthenticationService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHashing _passwordHashing;
        public AuthenticationService(IUserRepository userRepository, IJwtService jwtService, IPasswordHashing passwordHashing)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _passwordHashing = passwordHashing;
        }
        public async Task<string?> AuthenticateAsync(string UserName, string UserPassword)
        {
            var user = await _userRepository.GetUserByUsernameAndPasswordAsync(UserName, UserPassword);

            if (user == null)
            {
                return null; 
            }
            
            var token = _jwtService.GenerateToken(user.UserID, user.UserName);

            return token;
        }
        public async Task<bool> RegisterAsync(string userName, string userPassword)
        {
            var existingUser = await _userRepository.GetByUsername(userName);
            if (existingUser != null)
            {
                return false; 
            }
            string hashedPassword = _passwordHashing.HashPassword(userPassword);

            var newUser = new Users { UserName = userName, UserPassword = hashedPassword };
            await _userRepository.CreateAsync(newUser);

            return true; 
        }
        public async Task<string?> LoginAsync(string userName, string userPassword)
        {
            var user = await _userRepository.GetUserByUsernameAndPasswordAsync(userName, _passwordHashing.HashPassword(userPassword));

            if (user == null)
            {
                return null; 
            }

            var token = _jwtService.GenerateToken(user.UserID, user.UserName);
            return token;
        }
    }
}

