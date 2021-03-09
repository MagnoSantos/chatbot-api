using Chatbot.Domain.Models;
using System.Threading.Tasks;

namespace Chatbot.Domain.Interfaces
{
    public interface IMessageProcessHandler
    {
        Task Handle(MessageProcess messageProcess);
    }
}