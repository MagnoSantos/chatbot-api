using System.Text.Json.Serialization;

namespace Chatbot.Domain.DTOs
{
    public class WebhookDto
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}