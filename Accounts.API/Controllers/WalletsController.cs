using Accounts.Domain.Entities;
using Accounts.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.API.Controllers
{
    [Route("api/wallets")]
    [ApiController]
    public class WalletsController : BaseController<Wallets>
    {
        private readonly IWalletRepository _walletRepository;

        public WalletsController(IWalletRepository walletRepository) : base(walletRepository)
        {
            _walletRepository = walletRepository;
        }
    }
}

