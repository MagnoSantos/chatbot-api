using Chatbot.Domain.Interfaces;
using Chatbot.Domain.Models;
using Chatbot.Infraestructure.ExternalServices;
using System.Threading.Tasks;

namespace Chatbot.Domain.Implementations
{
    public class MessageProcessHandler : IMessageProcessHandler
    {
        private readonly IWatsonAssistant _watsonAssistant;

        public MessageProcessHandler(IWatsonAssistant watsonAssistant)
        {
            _watsonAssistant = watsonAssistant;
        }

        public async Task Handle(MessageProcess messageProcess)
        {
            var inputConversation = new InputConversation
            {
                Input = new Input
                {
                    Text = messageProcess.Text
                }
            };

            var resposta = await _watsonAssistant.Talks(inputConversation);
            var textResponse = string.Join(",", resposta.Output?.Generic);
        }
    }
}