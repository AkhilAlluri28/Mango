using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Text;

namespace Mango.MessageBus
{
    public class MessageBus : IMessageBus
    {
        // Ideally this need to be configured in appSettings file.
        string _connectionString = "";
        public async Task PublishMessage(object message, string queue_topic_name)
        {
            await using ServiceBusClient client = new(_connectionString);
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
