using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Chatbot.Domain.DTOs
{
    public class WebhookDto
    {
        [JsonPropertyName("object")]
        public string Object { get; set; }

        [JsonPropertyName("entry")]
        public IList<Entry> Entry { get; set; }
    }

    public class Entry
    {
        [JsonPropertyName("id")]
        public string PageId { get; set; }

        [JsonPropertyName("time")]
        public long Time { get; set; }

        [JsonPropertyName("messaging")]
        public IList<Messaging> Messaging { get; set; }
    }

    public class Messaging
    {
        [JsonPropertyName("sender")]
        public Sender Sender { get; set; }

        [JsonPropertyName("recipient")]
        public Recipient Recipient { get; set; }

        [JsonPropertyName("timestamp")]
        public string TimeStamp { get; set; }

        [JsonPropertyName("message")]
        public Message Message { get; set; }
    }

    public class Sender
    {
        [JsonPropertyName("id")]
        public string Psid { get; set; }
    }

    public class Recipient
    {
        [JsonPropertyName("id")]
        public string PageId { get; set; }
    }

    public class Message
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}