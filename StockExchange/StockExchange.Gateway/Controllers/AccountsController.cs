using Microsoft.AspNetCore.Mvc;

namespace StockExchange.Gateway.Controllers
{
    public class AccountsController : ApplicationBaseController
    {
        [HttpPost("Login")]
        public IActionResult Login()
        {
            return this.NoContent();
        }

        [HttpPost("Register")]
        public IActionResult Register()
        {
            return this.NoContent();
        }
        [HttpGet("FindByID")]
        public IActionResult FindByID(int id)
        {
            return this.NoContent();
        }
    }
}
