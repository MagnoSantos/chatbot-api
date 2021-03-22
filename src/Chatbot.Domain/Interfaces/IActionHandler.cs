using Chatbot.Infraestructure.ExternalServices;
using System.Threading.Tasks;

namespace Chatbot.Domain.Interfaces
{
    public interface IActionHandler
    {
        Task Handle(OutputConversation output);
    }
}