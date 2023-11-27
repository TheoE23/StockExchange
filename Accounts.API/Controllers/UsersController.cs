using Accounts.Domain.Entities;
using Accounts.Domain.Interfaces;
using Accounts.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : BaseController<Users>
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository) : base(userRepository)
        {
            _userRepository = userRepository;
        }
    }
}
