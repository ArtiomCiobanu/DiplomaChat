using System.Diagnostics.CodeAnalysis;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DiplomaChat.Common.Infrastructure.MessageQueueing.MessageQueueing.RabbitMQ
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class RabbitMqReader : IMessageQueueReader
    {
        private readonly IModel _channel;
        private readonly string _queueName;

        private IBasicConsumer _consumer;

        public RabbitMqReader(
            IModel channel,
            string queueName)
        {
            _channel = channel;
            _queueName = queueName;

            _channel.QueueDeclare(
                queue: _queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        public void SetReceiveAction(Action<string> receivedMessageAction)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();

                var rawString = Encoding.UTF8.GetString(body);

                receivedMessageAction(rawString);
            };

            _consumer = consumer;
        }

        public void StartReading()
        {
            _channel.BasicConsume(
                queue: _queueName,
                autoAck: true,
                consumer: _consumer);
        }
    }
}