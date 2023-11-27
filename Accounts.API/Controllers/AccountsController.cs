using Accounts.Domain.Entities;
using Accounts.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.API.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : BaseController<Account>
    {
        private readonly IAccountRepository _accountRepository;

        public AccountsController(IAccountRepository accountRepository) : base(accountRepository)
        {
            _accountRepository = accountRepository;
        }
    }
}

