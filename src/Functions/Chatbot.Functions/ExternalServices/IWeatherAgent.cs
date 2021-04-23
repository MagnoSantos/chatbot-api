using System.Threading.Tasks;

namespace Chatbot.Functions.ExternalServices
{
    public interface IWeatherAgent
    {
        Task<HGWeatherResponse> GetInformationByCity(string cityName);
    }
}