using Chatbot.Domain.Interfaces;
using Chatbot.Infraestructure.ExternalServices;
using System.Threading.Tasks;

namespace Chatbot.Domain.Implementations
{
    public class ActionHandler : IActionHandler
    {
        private readonly IActionFactory _actionFactory;

        public ActionHandler(IActionFactory actionFactory)
        {
            _actionFactory = actionFactory;
        }

        public async Task Handle(OutputConversation output)
        {
            var actions = output.Output.Context.Actions;

            foreach (var action in actions)
            {
                var actionToBeProcess = _actionFactory.Criar(action.Name);
                await actionToBeProcess?.ExecuteAsync(output);
            }
        }
    }
}