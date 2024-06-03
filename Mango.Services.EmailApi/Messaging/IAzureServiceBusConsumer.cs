namespace Mango.Services.EmailApi.Messaging
{
    /// <summary>
    /// This is responsible for processesing the service bus messages.
    /// </summary>
    public interface IAzureServiceBusConsumer
    {
        Task Start();
        Task Stop();
    }
}
