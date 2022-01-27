namespace DiplomaChat.Common.MessageQueueing.MessageQueueing
{
    public interface IMessageQueueConnectionFactory
    {
        public IMessageQueueConnection CreateConnection();
    }
}