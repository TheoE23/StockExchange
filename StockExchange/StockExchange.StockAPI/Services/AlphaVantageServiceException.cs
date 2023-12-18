namespace StockAPI.Services
{
    public class AlphaVantageServiceException : Exception
    {
        public AlphaVantageServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
