using Chatbot.Domain.DTOs;
using Chatbot.Domain.Interfaces;
using Chatbot.Domain.Models;
using System.Threading.Tasks;

namespace Chatbot.Domain.Implementations
{
    public class WebhookHandler : IWebhookHandler
    {
        private readonly IWeatherHandler _weatherHandler;

        public WebhookHandler(IWeatherHandler weatherHandler)
        {
            _weatherHandler = weatherHandler;
        }

        public async Task<string> Handle(WebhookDto webhookDto)
        {
            var messageProcess = new MessageProcess(webhookDto.City, webhookDto.Account);

            return await _weatherHandler.Handle(messageProcess.City);
        }
    }
}