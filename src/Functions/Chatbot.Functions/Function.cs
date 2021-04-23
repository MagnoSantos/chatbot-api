using Chatbot.Functions.Dtos;
using Chatbot.Functions.ExternalServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Chatbot.Functions
{
    public class Function
    {
        private readonly IWeatherAgent _weatherAgent;

        public Function(IWeatherAgent weatherAgent)
        {
            _weatherAgent = weatherAgent;
        }

        [FunctionName("Webhook")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "v1/webhook")] WebhookDto webhookDto,
            ILogger log
        )
        {
            try
            {
                log.LogInformation("[Function - Process Request]");

                var cityName = webhookDto?.City;
                var response = await _weatherAgent.GetInformationByCity(cityName);

                var formattedResponse = $"No dia {response.Results.Date} às {response.Results.Time} a temperatura marca {response.Results.Temp} graus. " +
                                        $"Está {response.Results.Description.ToLower()}!";

                return new OkObjectResult(new { retorno = formattedResponse });
            }
            catch (Exception ex)
            {
                log.LogError("[Function - Process Failed]", ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}