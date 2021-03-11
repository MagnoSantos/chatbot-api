using Chatbot.Infraestructure.ExternalServices.Options;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using System.Net.Mime;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chatbot.Infraestructure.ExternalServices
{
    public class WatsonAssistantAuth : IWatsonAssistantAuth
    {
        private readonly WatsonAssistantOptions _options;

        public WatsonAssistantAuth(IOptions<WatsonAssistantOptions> options)
        {
            _options = options.Value;
        }

        public async Task<string> GenerateToken()
        {
            var resposta = await _options.UrlAuth
                .AppendPathSegment("token")
                .WithHeader("Content-Type", "application/x-www-form-urlencoded")
                .WithHeader("Accept", MediaTypeNames.Application.Json)
                .PostUrlEncodedAsync(new
                {
                    grant_type = "urn:ibm:params:oauth:grant-type:apikey",
                    apikey = _options.ApiKey
                })
                .ReceiveJson<IamTokenResponse>();

            return resposta.Token;
        }

        public class IamTokenResponse
        {
            [JsonPropertyName("access_token")]
            public string Token { get; set; }

            [JsonPropertyName("refresh_token")]
            public string RefreshToken { get; set; }

            [JsonPropertyName("token_type")]
            public string TokenType { get; set; }

            [JsonPropertyName("expires_in")]
            public long ExpiresIn { get; set; }

            [JsonPropertyName("expiration")]
            public long Expiration { get; set; }

            [JsonPropertyName("scope")]
            public string Scope { get; set; }
        }
    }
}