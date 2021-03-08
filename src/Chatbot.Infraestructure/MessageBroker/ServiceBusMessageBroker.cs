using Azure.Messaging.ServiceBus;
using Chatbot.Domain.Interfaces;
using Chatbot.Domain.Models;
using Chatbot.Infraestructure.MessageBroker.Options;
using Microsoft.Extensions.Options;
using System;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;

namespace Chatbot.Infraestructure.MessageBroker
{
    public class ServiceBusMessageBroker : IClientMessageBroker, IAsyncDisposable
    {
        private readonly ServiceBusClient _client;

        public ServiceBusMessageBroker(IOptions<ServiceBusOptions> options)
        {
            _client = new ServiceBusClient(options.Value.ConnectionString);
        }

        public Task PublishMessageOnQueue(string queueName, MessageProcess messageProcess)
        {
            var sender = _client.CreateSender(queueName);

            var paylaodEncoded = JsonSerializer.Serialize(messageProcess);

            return sender.SendMessageAsync(
                new ServiceBusMessage(paylaodEncoded)
                {
                    ContentType = MediaTypeNames.Application.Json
                }
            );
        }

        public ValueTask DisposeAsync()
        {
            return _client.DisposeAsync();
        }
    }
}