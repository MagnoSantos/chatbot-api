using Chatbot.Domain.Interfaces;
using Chatbot.Domain.Models;
using Chatbot.Infraestructure.ExternalServices;
using System.Threading.Tasks;

namespace Chatbot.Domain.Implementations
{
    public class MessageProcessHandler : IMessageProcessHandler
    {
        private readonly IWatsonAssistant _watsonAssistant;
        private readonly IActionHandler _actionHandler;

        public MessageProcessHandler(IWatsonAssistant watsonAssistant, 
                                     IActionHandler actionHandler)
        {
            _watsonAssistant = watsonAssistant;
            _actionHandler = actionHandler;
        }

        public async Task<string> Handle(MessageProcess messageProcess)
        {
            var inputConversation = new InputConversation
            {
                Input = new Input
                {
                    Text = messageProcess.Text
                }
            };

            var response = await _watsonAssistant.Talks(inputConversation);

            await _actionHandler.Handle("", response);

            return string.Join(",", response.Output?.Generic);
        }
    }
}