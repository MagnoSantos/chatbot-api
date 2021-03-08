using Chatbot.Domain.Models;
using System.Threading.Tasks;

namespace Chatbot.Domain.Interfaces
{
    public interface IClientMessageBroker
    {
        Task PublishMessageOnQueue(string queueName, MessageProcess messageProcess);
    }
}