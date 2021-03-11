using System.Threading.Tasks;

namespace Chatbot.Infraestructure.ExternalServices
{
    public interface IWatsonAssistant
    {
        Task<OutputConversation> Talks(InputConversation inputConversation);
    }
}