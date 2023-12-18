using Newtonsoft.Json;

namespace StockAPI.Models.ApiResponses
{
    public class DailyApiResponse
    {
        [JsonProperty("Meta Data")]
        public MetaData MetaData { get; set; }

        [JsonProperty("Time Series (Daily)")]
        public Dictionary<string, TimeSeries> DailyTimeSeries { get; set; }
    }
}
