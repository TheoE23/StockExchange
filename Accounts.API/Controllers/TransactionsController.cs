using Accounts.Domain.Entities;
using Accounts.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.API.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionsController : BaseController<Transactions>
    {
        public TransactionsController(ITransactionRepository repository) : base(repository)
        {
        }
    }


}
