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
        private readonly IWebhookHandler _webhookHandler;

        public WebhookController(ILogger<WebhookController> logger,
                                 IWebhookHandler webhookHandler)
        {
            _logger = logger;
            _webhookHandler = webhookHandler;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> MessageProcess([FromBody] WebhookDto webhookDto)
        {
            _logger.LogInformation("Mensagem recebida no webhook", webhookDto);

            await _webhookHandler.Handle(webhookDto);
            
            return Ok();
        }
    }
}