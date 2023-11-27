using Accounts.Domain.Entities;
using Accounts.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.API.Controllers
{
    [Route("api/stocks")]
    [ApiController]
    public class StocksController : BaseController<Stocks>
    {
        private readonly IStockRepository _stockRepository;

        public StocksController(IStockRepository stockRepository) : base(stockRepository)
        {
            _stockRepository = stockRepository;
        }
    }
}

