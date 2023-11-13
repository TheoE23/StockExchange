using Accounts.Domain.Entities;
using Accounts.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : BaseController<Users>
    {
        public UsersController(IUserRepository repository) : base(repository)
        {
        }



    }
   }
