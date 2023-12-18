using StockAPI.Models;
using StockAPI.Models.ApiResponses;
using Newtonsoft.Json;
using System.Data.SQLite;
using StockAPI.Data;

namespace StockAPI.Services
{
    public class StockAPIService : IStockAPIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly SqliteHelper _sqliteHelper;

        public StockAPIService(IHttpClientFactory httpClientFactory, string apiKey, SqliteHelper sqliteHelper)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://www.alphavantage.co/");
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            _sqliteHelper = sqliteHelper ?? throw new ArgumentNullException(nameof(sqliteHelper));
        }

        private async Task<string> MakeApiRequestAsync(string endpoint)
        {
            try
            {
                var response = await _httpClient.GetStringAsync(endpoint);
                return response;
            }
            catch (HttpRequestException ex)
            {
                //handle HTTP request errors
                throw new AlphaVantageServiceException("Error communicating with Alpha Vantage API. Endpoint: {endpoint}", ex);
            }
            catch (JsonException ex)
            {
                //handle JSON serialization/deserialization errors
                throw new AlphaVantageServiceException("Error processing Alpha Vantage API response.", ex);
            }
        }

        public async Task<IntradayApiResponse> GetIntradayDataAsync(string symbol)
        {
            string interval = "5min";
            string apiUrl = $"query?function=TIME_SERIES_INTRADAY&symbol={symbol}&interval={interval}&apikey={_apiKey}";

            var response = await MakeApiRequestAsync(apiUrl);
            var apiResponse = JsonConvert.DeserializeObject<IntradayApiResponse>(response);

            return apiResponse;
        }

        private void HandleDailyTimeSeries(DailyApiResponse apiResponse, DateTime date)
        {
            //Filter the TimeSeries data for the specific date
            if (apiResponse?.DailyTimeSeries != null)
            {
                apiResponse.DailyTimeSeries = apiResponse.DailyTimeSeries
                    .Where(entry => DateTime.Parse(entry.Key).Date == date.Date)
                    .ToDictionary(entry => entry.Key, entry => entry.Value);
            }
        }

        public async Task<DailyApiResponse> GetDailyDataAsync(string symbol, DateTime date)
        {
            string apiUrl = $"query?function=TIME_SERIES_DAILY&symbol={symbol}&apikey={_apiKey}";
            var response = await MakeApiRequestAsync(apiUrl);

            var apiResponse = JsonConvert.DeserializeObject<DailyApiResponse>(response);
            HandleDailyTimeSeries(apiResponse, date);

            return apiResponse;
        }

        public async Task<MonthlyApiResponse> GetMonthlyDataAsync(string symbol)
        {
            using (var connection = _sqliteHelper.GetConnection())
            {
                connection.Open();
                string allDataQuery = $"SELECT * FROM MonthlyData WHERE Symbol = '{symbol}'";

                using (var allDataCommand = new SQLiteCommand(allDataQuery, connection))
                using (var reader = allDataCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        //data exists in the database, construct ApiResponse and return
                        var storedData = new Dictionary<string, TimeSeries>();
                        while (reader.Read())
                        {
                            string date = reader["Date"].ToString();
                            storedData[date] = new TimeSeries
                            {
                                Open = reader["Open"].ToString(),
                                High = reader["High"].ToString(),
                                Low = reader["Low"].ToString(),
                                Close = reader["Close"].ToString(),
                                Volume = reader["Volume"].ToString()
                            };
                        }

                        var monthlyResponse = new MonthlyApiResponse
                        {
                            MetaData = new MetaData
                            {
                                Symbol = symbol,
                            },
                            MonthlyTimeSeries = storedData
                        };

                        return monthlyResponse;
                    }
                }
                //url
                string apiUrl = $"query?function=TIME_SERIES_MONTHLY&symbol={symbol}&apikey={_apiKey}";

                var response = await MakeApiRequestAsync(apiUrl);
                var apiResponse = JsonConvert.DeserializeObject<MonthlyApiResponse>(response);

                //storing the Data
                StoreMonthlyDataInDatabase(apiResponse);

                return apiResponse;
            }
        }

        private void StoreMonthlyDataInDatabase(MonthlyApiResponse monthlyApiResponse)
        {
            using (var connection = _sqliteHelper.GetConnection())
            {
                connection.Open();

                foreach (var monthlyData in monthlyApiResponse.MonthlyTimeSeries)
                {
                    string date = monthlyData.Key;

                    //checking if data for the same month already exists
                    string checkQuery = $"SELECT COUNT(*) FROM MonthlyData WHERE Symbol = '{monthlyApiResponse.MetaData.Symbol}' AND Date = '{date}'";

                    using (var checkCommand = new SQLiteCommand(checkQuery, connection))
                    {
                        int existingRowCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                        // If no data exists, insert the new data
                        if (existingRowCount == 0)
                        {
                            string insertQuery = $"INSERT INTO MonthlyData (Symbol, Date, Open, High, Low, Close, Volume) VALUES " +
                                                $"'{monthlyApiResponse.MetaData.Symbol}', '{date}', " +
                                                $"'{monthlyData.Value.Open}', '{monthlyData.Value.High}', " +
                                                $"'{monthlyData.Value.Low}', '{monthlyData.Value.Close}', " +
                                                $"'{monthlyData.Value.Volume}')";

                            _sqliteHelper.ExecuteNonQuery(insertQuery, connection);
                        }
                    }
                }
            }
        }
    }
}

