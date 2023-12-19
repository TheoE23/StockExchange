using Accounts.Domain.Abstraction.Services;
using Accounts.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Accounts.Domain.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        public bool Authorize(string token, string requiredRole)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var roleClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role);

            if (roleClaim == null)
            {
                throw new Exception("Role not found.");
            }

            return roleClaim.Value == requiredRole;
        }
    }
}
