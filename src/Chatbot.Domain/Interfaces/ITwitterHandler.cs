using System.Threading.Tasks;

namespace Chatbot.Domain.Interfaces
{
    public interface ITwitterHandler
    {
        Task<string> Handle(string account);
    }
}