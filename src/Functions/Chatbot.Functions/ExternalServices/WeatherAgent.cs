using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using Polly;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chatbot.Functions.ExternalServices
{
    public class WeatherAgent : IWeatherAgent
    {
        private readonly WeatherOptions _options;

        public WeatherAgent(IOptionsMonitor<WeatherOptions> options)
        {
            _options = options.CurrentValue;
        }

        public async Task<HGWeatherResponse> GetInformationByCity(string cityName)
        {
            return await Policy
                .Handle<FlurlHttpException>()
                .RetryAsync()
                .ExecuteAsync(() =>
                    _options.UrlBase
                        .SetQueryParam("key", _options.ApiKey)
                        .SetQueryParam("city_name", cityName)
                        .GetJsonAsync<HGWeatherResponse>()
                );
        }
    }

    public class HGWeatherResponse
    {
        [JsonPropertyName("results")]
        public Results Results { get; set; }
    }

    public class Results
    {
        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("time")]
        public string Time { get; set; }

        [JsonPropertyName("temp")]
        public int Temp { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}