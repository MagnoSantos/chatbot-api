using System.Text.Json.Serialization;

namespace Chatbot.Functions.Dtos
{
    public class WebhookDto
    {
        [JsonPropertyName("city")]
        public string City { get; set; }
    }
}