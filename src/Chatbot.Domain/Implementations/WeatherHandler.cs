using Chatbot.Domain.Interfaces;
using Chatbot.Infraestructure.ExternalServices;
using System.Threading.Tasks;

namespace Chatbot.Domain.Implementations
{
    public class WeatherHandler : IWeatherHandler
    {
        private readonly IHGWeather _hgWeather;

        public WeatherHandler(IHGWeather hGWeather)
        {
            _hgWeather = hGWeather;
        }

        public async Task<string> Handle(string cityName)
        {
            var response = await _hgWeather.GetWeatherInformationByCity(cityName);

            return $"No dia {response.Results.Date} às {response.Results.Time} a temperatura marca {response.Results.Temp} graus. " +
                   $"O céu está {response.Results.Description.ToLower()}";
        }
    }
}