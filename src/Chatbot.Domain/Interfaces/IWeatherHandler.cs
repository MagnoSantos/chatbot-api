using System.Threading.Tasks;

namespace Chatbot.Domain.Interfaces
{
    public interface IWeatherHandler 
    {
        Task<string> Handle(string cityName);
    }
}