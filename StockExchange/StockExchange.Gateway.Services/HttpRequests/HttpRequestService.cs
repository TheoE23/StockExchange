using System.Net.Http.Json;

namespace StockExchange.Gateway.Services.HttpRequests
{
    public class HttpRequestService : IHttpRequestService
    {
        private readonly HttpClient httpClient;

        public HttpRequestService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public Task<T?> GetAsync<T>(string url)
            => this.httpClient.GetFromJsonAsync<T>(url);

        public Task PostAsync<T>(string url, T body)
            => this.httpClient.PostAsJsonAsync(url, body);

        public Task PutAsync<T>(string url, T body)
            => this.httpClient.PutAsJsonAsync(url, body);

        public Task DeleteAsync(string url)
            => this.httpClient.DeleteAsync(url);
    }
}
