namespace Chatbot.Domain.Models
{
    public class MessageProcess
    {
        public string Psid { get; set; }
        public string Text { get; set; }

        public MessageProcess(string psid, string text)
        {
            Psid = psid;
            Text = text;
        }
    }
}