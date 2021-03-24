using Chatbot.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Chatbot.Domain.Implementations
{
    public class ActionFactory : IActionFactory
    {
        private readonly IEnumerable<IAction> _actions;

        public ActionFactory(IEnumerable<IAction> actions)
        {
            _actions = actions;
        }

        public IAction Criar(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            return _actions.FirstOrDefault(action => action.Name == name);
        }
    }
}