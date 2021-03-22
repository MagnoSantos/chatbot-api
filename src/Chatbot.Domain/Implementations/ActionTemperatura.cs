using Chatbot.Domain.Interfaces;
using Chatbot.Infraestructure.ExternalServices;
using System.Threading.Tasks;

namespace Chatbot.Domain.Implementations
{
    public class ActionTemperatura : IAction
    {
        public string Name => "tempProcess";

        public bool ProcessWatsonAfterExecution => true;

        public Task Handle(string name, OutputConversation output)
        {
            throw new System.NotImplementedException();
        }
    }
}