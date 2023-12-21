using Microsoft.AspNetCore.Mvc;

namespace StockExchange.Gateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ApplicationBaseController : ControllerBase
    {
    }
}
