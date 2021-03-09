using Azure.Messaging.ServiceBus;
using Chatbot.Domain.Interfaces;
using Chatbot.Domain.Models;
using Chatbot.Infraestructure.MessageBroker.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Chatbot.API.HostedServices
{
    public class MessageProcessHostedService : IHostedService
    {
        private readonly ServiceBusClient _client;
        private readonly ServiceBusProcessor _processor;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MessageProcessHostedService> _logger;

        public MessageProcessHostedService(
            IServiceProvider serviceProvider,
            IOptions<ServiceBusOptions> options,
            ILogger<MessageProcessHostedService> logger)
        {
            _client = new ServiceBusClient(options.Value.ConnectionString);
            _processor = _client.CreateProcessor("to-whatsapp", new ServiceBusProcessorOptions
            {
                AutoCompleteMessages = true,
                ReceiveMode = ServiceBusReceiveMode.ReceiveAndDelete
            });

            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _processor.ProcessMessageAsync += OnMessage;
            _processor.ProcessErrorAsync += OnError;

            await _processor.StartProcessingAsync(cancellationToken);
        }

        private Task OnError(ProcessErrorEventArgs error)
        {
            _logger.LogError(error.Exception, "Falha ao processar mensagem via service bus");
            return Task.CompletedTask;
        }

        private async Task OnMessage(ProcessMessageEventArgs msg)
        {
            var payload = JsonSerializer.Deserialize<MessageProcess>(msg.Message.Body.ToString());

            using var scope = _serviceProvider.CreateScope();

            var handler = scope.ServiceProvider.GetRequiredService<IMessageProcessHandler>();

            await handler.Handle(payload);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _processor.StopProcessingAsync(cancellationToken);
            await _processor.CloseAsync(cancellationToken);
            await _client.DisposeAsync();
        }
    }
}
