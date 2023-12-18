using StockAPI.Models.ApiResponses;

namespace StockAPI.Services
{
    public interface IStockAPIService
    {
        Task<IntradayApiResponse> GetIntradayDataAsync(string symbol);
        Task<MonthlyApiResponse> GetMonthlyDataAsync(string symbol);
        Task<DailyApiResponse> GetDailyDataAsync(string symbol, DateTime date);
    }
}
