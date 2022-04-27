namespace DiplomaChat.Common.Infrastructure.MessageQueueing.MessageQueueing
{
    public interface IMessageQueueConnectionFactory
    {
        public IMessageQueueConnection CreateConnection();
    }
}