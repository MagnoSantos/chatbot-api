using Chatbot.Domain.DTOs;
using System.Threading.Tasks;

namespace Chatbot.Domain.Interfaces
{
    public interface IWebhookHandler
    {
        Task<string> Handle(WebhookDto webhookDto);
    }
}