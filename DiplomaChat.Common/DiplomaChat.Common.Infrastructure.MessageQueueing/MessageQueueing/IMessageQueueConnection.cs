namespace DiplomaChat.Common.Infrastructure.MessageQueueing.MessageQueueing
{
    public interface IMessageQueueConnection : IDisposable
    {
        public IMessageQueuePublisher CreatePublisher(string queueName);

        public IMessageQueueReader CreateReader(string queueName);
    }
}