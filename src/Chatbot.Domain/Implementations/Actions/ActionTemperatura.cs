using Chatbot.Domain.Interfaces;
using Chatbot.Infraestructure.ExternalServices;
using System.Linq;
using System.Threading.Tasks;

namespace Chatbot.Domain.Implementations.Actions
{
    public class ActionTemperatura : IAction
    {
        public string Name => "tempProcess";

        public bool ProcessWatsonAfterExecution => true;

        private readonly IHGWeather _hgWeater;

        public ActionTemperatura(IHGWeather hGWeater)
        {
            _hgWeater = hGWeater;
        }

        public async Task ExecuteAsync(OutputConversation output)
        {
            var nameCity = output.Output?.Context?.City;

            var response = await _hgWeater.GetWeatherInformationByCity(nameCity);

            if (response == null) return;

            var formattedResponse = $"No dia {response.Results.Date} às {response.Results.Time} a tempetura marca {response.Results.Temp} graus. " +
                                    $"O céu está {response.Results.Description}";

            output.Output.Generic.First().Text = formattedResponse;
        }
    }
}