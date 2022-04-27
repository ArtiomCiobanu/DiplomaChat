namespace DiplomaChat.Common.Infrastructure.MessageQueueing.MessageQueueing
{
    public interface IMessageQueueReader
    {
        public void SetReceiveAction(Action<string> receivedMessageAction);
        public void StartReading();
    }
}