using Chatbot.Infrastructure.ExternalServices.HG_Weater;
using System.Threading.Tasks;

namespace Chatbot.Infraestructure.ExternalServices
{
    public interface IHGWeather
    {
        Task<HGWeatherResponse> GetWeatherInformationByCity(string name);
    }
}