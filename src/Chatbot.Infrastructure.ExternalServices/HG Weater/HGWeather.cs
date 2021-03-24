using Chatbot.Infraestructure.ExternalServices;
using Chatbot.Infrastructure.ExternalServices.HG_Weater.Options;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chatbot.Infrastructure.ExternalServices.HG_Weater
{
    public class HGWeather : IHGWeather
    {
        private readonly HGWeatherOptions _options;

        public HGWeather(IOptionsMonitor<HGWeatherOptions> options)
        {
            _options = options.CurrentValue;
        }
        
        public async Task<HGWeatherResponse> GetWeatherInformationByCity(string name)
        {
            return await _options.UrlBase
                .SetQueryParam("key", _options.ApiKey)
                .SetQueryParam("city_name", name)
                .GetJsonAsync<HGWeatherResponse>();
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