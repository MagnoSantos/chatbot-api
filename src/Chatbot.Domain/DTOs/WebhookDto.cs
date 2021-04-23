using System.Text.Json.Serialization;

namespace Chatbot.Domain.DTOs
{
    public class WebhookDto
    {
        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("account")]
        public string Account { get; set; }
    }
}