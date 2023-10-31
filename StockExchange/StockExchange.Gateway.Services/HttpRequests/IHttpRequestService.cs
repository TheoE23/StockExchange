namespace StockExchange.Gateway.Services.HttpRequests
{
    public interface IHttpRequestService
    {
        public Task<T?> GetAsync<T>(string url);

        public Task PostAsync<T>(string url, T body);

        public Task PutAsync<T>(string url, T body);

        public Task DeleteAsync(string url);
    }
}
