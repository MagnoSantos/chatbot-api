using Chatbot.API.Options;
using Chatbot.Domain.DTOs;
using Chatbot.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Chatbot.API.Controllers
{
    [Route("api/v1/webhook")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class WebhookController : ControllerBase
    {
        private readonly ILogger<WebhookController> _logger;
        private readonly WebhookOptions _options;
        private readonly IWebhookHandler _webhookHandler;

        public WebhookController(ILogger<WebhookController> logger,
                                 IOptions<WebhookOptions> options, 
                                 IWebhookHandler webhookHandler)
        {
            _logger = logger;
            _webhookHandler = webhookHandler;
            _options = options.Value;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult WebhookSubscribe(HttpRequest httpRequest)
        {
            string mode = httpRequest.Query["hub.mode"];
            string token = httpRequest.Query["hub.verify_token"];
            string challenge = httpRequest.Query["hub.challenge"];

            if (mode == null && token == null) return BadRequest();

            _logger.LogInformation("Registro da página Facebook Messenger", new Dictionary<string, string>
            {
                ["Modo"] = mode,
                ["TokenVerificacao"] = token,
                ["Desafio"] = challenge
            });

            if (mode == "subscribe" && token == _options.VerifyToken)
            {
                return new OkObjectResult(challenge);
            }
            else
            {
                return new StatusCodeResult(StatusCodes.Status403Forbidden);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> MessageProcess([FromBody] WebhookDto webhookDto)
        {
            await _webhookHandler.Handle(webhookDto);
            
            return Ok();
        }
    }
}