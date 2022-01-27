using System;

namespace DiplomaChat.Common.MessageQueueing.MessageQueueing
{
    public interface IMessageQueuePublisher : IDisposable
    {
        public void PublishMessage<TBody>(TBody messageBody);
    }
}