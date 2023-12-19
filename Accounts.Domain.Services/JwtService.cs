using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Accounts.Domain.Abstraction.Services;
using Accounts.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Accounts.Domain.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserServices _userServices;
        private readonly IAccountRepository _accountRepository;

        public JwtService(IConfiguration configuration, IUserServices userServices, IAccountRepository accountRepository)
        {
            _configuration = configuration;
            _userServices = userServices;
            _accountRepository = accountRepository;
        }

        public string GenerateToken(int UserID, string UserName)
        {
            var user = _userServices.GetByIdAsync(UserID).Result;

            if (user == null)
            {
                return "User not found";
            }

            var accountBalance = _accountRepository.GetBalanceAsync(UserID).Result;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: new List<Claim>
                {
                new Claim(ClaimTypes.NameIdentifier, user.ToString()),
                new Claim(ClaimTypes.Name, UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.DateOfBirth, user.Birthday.ToString("yyyy-MM-dd")),
               
                },
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private string GetUserRole(decimal accountBalance)
        {
            if (accountBalance <= 1000)
            {
                return "Usual";
            }
            else if (accountBalance > 1000 && accountBalance <= 10000)
            {
                return "Advanced";
            }
            else
            {
                return "Master";
            }
        }
    }
}
