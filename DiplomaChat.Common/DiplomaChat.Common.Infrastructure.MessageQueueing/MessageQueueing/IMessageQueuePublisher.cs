namespace DiplomaChat.Common.Infrastructure.MessageQueueing.MessageQueueing
{
    public interface IMessageQueuePublisher : IDisposable
    {
        public void PublishMessage<TBody>(TBody messageBody);
    }
}