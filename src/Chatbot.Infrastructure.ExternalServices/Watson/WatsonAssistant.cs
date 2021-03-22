using Chatbot.Infraestructure.ExternalServices.Options;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chatbot.Infraestructure.ExternalServices
{
    public class WatsonAssistant : IWatsonAssistant
    {
        private readonly IWatsonAssistantAuth _auth;
        private readonly WatsonAssistantOptions _options;

        public WatsonAssistant(IWatsonAssistantAuth auth,
                               IOptions<WatsonAssistantOptions> options)
        {
            _auth = auth;
            _options = options.Value;
        }

        public async Task<OutputConversation> Talks(InputConversation inputConversation)
        {
            var token = await _auth.GenerateToken();

            return await _options.UrlWatson
                .AppendPathSegment(ServiceSendMessage(_options.AssistantId))
                .SetQueryParam("version", _options.Version)
                .WithOAuthBearerToken(token)
                .PostJsonAsync(inputConversation)
                .ReceiveJson<OutputConversation>();
        }

        private string ServiceSendMessage(string assistantId) => $"v2/assistants/{assistantId}/message";
    }

    public class InputConversation
    {
        [JsonPropertyName("input")]
        public Input Input { get; set; }
    }

    public class Input
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }

    public class OutputConversation
    {
        [JsonPropertyName("output")]
        public Output Output { get; set; }
    }

    public class Output
    {
        [JsonPropertyName("generic")]
        public IList<Generic> Generic { get; set; }

        [JsonPropertyName("context")]
        public Context Context { get; set; }
    }

    public class Generic
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }

    public class Context
    {
        [JsonPropertyName("actions")]
        public IList<DialogNodeAction> Actions { get; set; }
    }

    public class DialogNodeAction
    {
        [JsonPropertyName("name")
        public string Name { get; set; }
    }
}