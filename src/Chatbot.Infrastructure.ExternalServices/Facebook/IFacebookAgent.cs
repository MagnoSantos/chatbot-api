using System.Threading.Tasks;
using static Chatbot.Infrastructure.ExternalServices.Facebook.FacebookAgent;

namespace Chatbot.Infrastructure.ExternalServices.Facebook
{
    public interface IFacebookAgent
    {
        Task SendMessage(MessageFacebookInput message);
    }
}