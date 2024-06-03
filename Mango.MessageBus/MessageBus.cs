using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Text;

namespace Mango.MessageBus
{
    public class MessageBus : IMessageBus
    {
        private const string _serviceBusConnectionString = "";

        public async Task PublishMessage(object message, string queue_topic_name)
        {
            await using ServiceBusClient client = new(_serviceBusConnectionString);
            ServiceBusSender sender = client.CreateSender(queue_topic_name);

            string serializedMessage = JsonConvert.SerializeObject(message);

            ServiceBusMessage finalServiceBusMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(serializedMessage))
            {
                CorrelationId = Guid.NewGuid().ToString()
            };

            await sender.SendMessageAsync(finalServiceBusMessage);

            await client.DisposeAsync();
        }
    }
}
