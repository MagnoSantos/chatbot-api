using Chatbot.Domain.Interfaces;
using Chatbot.Domain.Models;
using Chatbot.Infraestructure.ExternalServices;
using Chatbot.Infrastructure.ExternalServices.Facebook;
using System.Linq;
using System.Threading.Tasks;
using static Chatbot.Infrastructure.ExternalServices.Facebook.FacebookAgent;

namespace Chatbot.Domain.Implementations
{
    public class MessageProcessHandler : IMessageProcessHandler
    {
        private readonly IWatsonAssistant _watsonAssistant;
        private readonly IFacebookAgent _facebookAgent;

        public MessageProcessHandler(IWatsonAssistant watsonAssistant,
                                     IFacebookAgent facebookAgent)
        {
            _watsonAssistant = watsonAssistant;
            _facebookAgent = facebookAgent;
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
            var text = resposta.Output?.Generic?.FirstOrDefault()?.Text;

            await _facebookAgent.SendMessage(new MessageFacebookInput
            {
                Recipient = new Recipient
                {
                    Psid = messageProcess.Psid
                },
                Message = new Message
                {
                    Text = text
                }
            });
        }
    }
}