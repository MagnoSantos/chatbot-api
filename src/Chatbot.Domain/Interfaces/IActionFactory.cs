namespace Chatbot.Domain.Interfaces
{
    public interface IActionFactory
    {
        IAction Criar(string name);
    }
}