namespace Chatbot.Domain.Models
{
    public class MessageProcess
    {
        public string Text { get; set; }

        public MessageProcess(string text)
        {
            Text = text;
        }
    }
}