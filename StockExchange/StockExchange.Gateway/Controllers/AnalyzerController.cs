using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using StockExchange.Gateway.Options;
using StockExchange.Gateway.Services.HttpRequests;

namespace StockExchange.Gateway.Controllers
{
    public class AnalyzerController : ApplicationBaseController
    {
        private readonly IHttpRequestService httpRequestService;
        private readonly IOptionsMonitor<EndpointsOptions> optionsMonitor;

        // https://learn.microsoft.com/en-us/dotnet/core/extensions/options
        public AnalyzerController(
            IHttpRequestService httpRequestService,
            IOptionsMonitor<EndpointsOptions> optionsMonitor)
        {
            this.httpRequestService = httpRequestService;
            this.optionsMonitor = optionsMonitor;
        }

        // This is temp data. 
        // TODO: implement real api call.
        [HttpGet]
        public async Task<IActionResult> Wallet(int id)
        {
            string endpoint = this.optionsMonitor.CurrentValue.Wallet;

            // This is just an example
            object? response = await this.httpRequestService.GetAsync<object>(string.Format(endpoint, id));

            if (response == null)
            {
                return this.NotFound();
            }

            return this.NoContent();
        }
    }
}
