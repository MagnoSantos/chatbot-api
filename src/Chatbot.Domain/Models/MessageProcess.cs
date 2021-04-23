namespace Chatbot.Domain.Models
{
    public class MessageProcess
    {
        public string City { get; set; }
        public string Account { get; set; }

        public MessageProcess(string city, string account)
        {
            City = city;
        }
    }
}