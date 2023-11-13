using Accounts.Domain.Entities;
using Accounts.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.API.Controllers
{
    [Route("api/accounts")]
        [ApiController]
    public class AccountsController : BaseController<Account>
    {
        
        public AccountsController(IAccountRepository repository) : base(repository)
        {
        }
    }

}
