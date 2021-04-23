using Chatbot.Domain.DTOs;
using Chatbot.Domain.Interfaces;
using Chatbot.Domain.Models;
using System.Threading.Tasks;

namespace Chatbot.Domain.Implementations
{
    public class WebhookHandler : IWebhookHandler
    {
        private readonly IWeatherHandler _weatherHandler;
        private readonly ITwitterHandler _twitterHandler;

        public WebhookHandler(IWeatherHandler weatherHandler,
                              ITwitterHandler twitterHandler)
        {
            _weatherHandler = weatherHandler;
            _twitterHandler = twitterHandler;
        }

        public async Task<string> Handle(WebhookDto webhookDto)
        {
            var messageProcess = new MessageProcess(webhookDto.City, webhookDto.Account);

            if (string.IsNullOrWhiteSpace(messageProcess.Account))
                return await _weatherHandler.Handle(messageProcess.City);

            return await _twitterHandler.Handle(webhookDto.Account);
        }
    }
}