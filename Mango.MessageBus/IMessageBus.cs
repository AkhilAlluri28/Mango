namespace Mango.MessageBus
{
    public interface IMessageBus
    {
        public Task PublishMessage(object message, string queue_topic_name);
    }
}
