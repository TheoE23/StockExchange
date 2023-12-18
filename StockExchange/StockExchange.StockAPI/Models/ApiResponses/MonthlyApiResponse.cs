using Newtonsoft.Json;

namespace StockAPI.Models.ApiResponses
{
    public class MonthlyApiResponse
    {
        [JsonProperty("Meta Data")]
        public MetaData MetaData { get; set; }

        [JsonProperty("Monthly Time Series")]
        public Dictionary<string, TimeSeries> MonthlyTimeSeries { get; set; }
    }
}
