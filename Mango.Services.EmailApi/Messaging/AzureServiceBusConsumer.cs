
using Azure.Messaging.ServiceBus;
using Mango.Services.EmailApi.Models.Dto;
using Mango.Services.EmailApi.Services;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;
using System.Text;

namespace Mango.Services.EmailApi.Messaging
{
    /// <inherit />
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly string _connectionString;
        private readonly string _queueName;
        private readonly IConfiguration _configuration;
        private readonly EmailService _emailService;

        private readonly ServiceBusProcessor _serviceBusProcessor;

        public AzureServiceBusConsumer(IConfiguration configuration, EmailService emailService)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            _queueName = _configuration.GetValue<string>("TopicAndQueueNames:EmailShoppingCartQueue");
            _emailService = emailService;

            var client = new ServiceBusClient(_connectionString);

            _serviceBusProcessor = client.CreateProcessor(_queueName);
        }

        public async Task Start()
        {
            _serviceBusProcessor.ProcessMessageAsync += OnEmailCartRequestReceived;
            _serviceBusProcessor.ProcessErrorAsync += ErrorHandler;

            await _serviceBusProcessor.StartProcessingAsync();
        }

        public async Task Stop()
        {
            await _serviceBusProcessor.StopProcessingAsync();
            await _serviceBusProcessor.DisposeAsync();
        }

        private async Task OnEmailCartRequestReceived(ProcessMessageEventArgs args)
        {
            try
            {
                ServiceBusReceivedMessage message = args.Message;
                string body = Encoding.UTF8.GetString(message.Body);

                CartDto cartDto = JsonConvert.DeserializeObject<CartDto>(body);

                await _emailService.EmailCartAndLog(cartDto);

                await args.CompleteMessageAsync(message);
                
            }catch(Exception ex)
            {
                // TODO: Need to log the exception details and error handling is performed here.
                //throw;
            }
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}
