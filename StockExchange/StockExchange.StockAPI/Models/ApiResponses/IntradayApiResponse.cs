using Newtonsoft.Json;

namespace StockAPI.Models.ApiResponses
{
    public class IntradayApiResponse
    {
        [JsonProperty("Meta Data")]
        public MetaData MetaData { get; set; }

        [JsonProperty("Time Series (5min)")]
        public Dictionary<string, TimeSeries> TimeSeries { get; set; }
    }
}
