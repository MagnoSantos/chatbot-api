using System.Threading.Tasks;

namespace Chatbot.Infraestructure.ExternalServices
{
    public interface IWatsonAssistantAuth
    {
        Task<string> GenerateToken();
    }
}