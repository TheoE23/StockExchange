﻿using Newtonsoft.Json;

namespace StockAPI.Models.ApiResponses
{
    public class TimeSeries
    {
        [JsonProperty("1. open")]
        public string Open { get; set; }

        [JsonProperty("2. high")]
        public string High { get; set; }

        [JsonProperty("3. low")]
        public string Low { get; set; }

        [JsonProperty("4. close")]
        public string Close { get; set; }

        [JsonProperty("5. volume")]
        public string Volume { get; set; }
    }
}
