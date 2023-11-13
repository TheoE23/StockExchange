using Accounts.Domain.Entities;
using Accounts.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.API.Controllers
{
    [Route("api/wallets")]
    [ApiController]
    public class WalletsController : BaseController<Wallets>
    {
        public WalletsController(IWalletRepository repository) : base(repository)
        {
        }
    }

}
