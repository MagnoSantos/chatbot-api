using Chatbot.Infrastructure.ExternalServices.Facebook.Options;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chatbot.Infrastructure.ExternalServices.Facebook
{
    public class FacebookAgent : IFacebookAgent
    {
        private readonly FacebookOptions _options;

        public FacebookAgent(IOptions<FacebookOptions> options)
        {
            _options = options.Value;
        }

        public async Task SendMessage(MessageFacebookInput message)
        {
            await _options.UrlBase
                .SetQueryParam("acess_token", _options.PageAcessToken)
                .PostJsonAsync(message);
        }

        public class MessageFacebookInput
        {
            [JsonPropertyName("messaging_type")]
            public string MessageType { get; set; } = "RESPONSE";

            [JsonPropertyName("recipient")]
            public Recipient Recipient { get; set; }

            [JsonPropertyName("message")]
            public Message Message { get; set; }
        }

        public class Recipient
        {
            [JsonPropertyName("id")]
            public string Psid { get; set; }
        }

        public class Message
        {
            [JsonPropertyName("text")]
            public string Text { get; set; }
        }
    }
}