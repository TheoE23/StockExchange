using Accounts.Domain.Entities;
using Accounts.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.API.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionsController : BaseController<Transactions>
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionsController(ITransactionRepository transactionRepository) : base(transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
    }
}

