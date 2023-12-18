using StockAPI.Services;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace StockAPI.Controllers
{
    public class ApiResponseController : Controller
    {

        private readonly IStockAPIService _stockAPIService;
        private readonly ILogger<ApiResponseController> _logger;

        public ApiResponseController(IStockAPIService stockAPIService, ILogger<ApiResponseController> logger)
        {
            _stockAPIService = stockAPIService ?? throw new ArgumentNullException(nameof(stockAPIService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        [HttpGet("GetIntradayData")]
        public async Task<IActionResult> GetIntradayData(string symbol)
        {
            try
            {
                var alphaVantageApiResponse = await _stockAPIService.GetIntradayDataAsync(symbol);

                return Ok(alphaVantageApiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error in GetIntradayData");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetMonthlyData")]
        public async Task<IActionResult> GetMonthlyData(string symbol)
        {
           try
           {
              var monthlyApiResponse = await _stockAPIService.GetMonthlyDataAsync(symbol);

              return Ok(monthlyApiResponse);
           }
           catch (Exception ex)
           {
                _logger.LogError(ex, "There was an error in GetMonthlyData");
                return StatusCode(500, $"Internal server error: {ex.Message}");
           }
        }

        [HttpGet("GetDailyData")]
        public async Task<IActionResult> GetDailyData(string symbol, DateTime date)
        {
            try
            {
                var dailyApiResponse = await _stockAPIService.GetDailyDataAsync(symbol, date);

                return Ok(dailyApiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error in GetDailyData");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
