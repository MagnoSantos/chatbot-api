using System;

namespace Chatbot.Infrastructure.ExternalServices.Facebook.Options
{
    public class FacebookOptions
    {
        public string UrlBase { get; set; }
        public string PageAcessToken { get; set; }
        public string VersaoApi { get; set; }
        public TimeSpan TimeOut { get; set; }
    }
}