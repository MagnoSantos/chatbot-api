namespace Chatbot.Infraestructure.ExternalServices.Options
{
    public class WatsonAssistantOptions
    {
        public string UrlAuth { get; set; }
        public string ApiKey { get; set; }
        public string UrlWatson { get; set; }
        public string AssistantId { get; set; }
        public string Version { get; set; }
    }
}