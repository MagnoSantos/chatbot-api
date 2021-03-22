using Chatbot.Infraestructure.ExternalServices;
using System.Threading.Tasks;

namespace Chatbot.Domain.Interfaces
{
    public interface IAction
    {
        string Name { get; }

        bool ProcessWatsonAfterExecution { get; }

        Task ExecuteAsync(OutputConversation output);
    }
}