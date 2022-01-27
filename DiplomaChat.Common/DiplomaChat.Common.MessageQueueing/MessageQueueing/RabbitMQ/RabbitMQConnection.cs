using System.Diagnostics.CodeAnalysis;
using RabbitMQ.Client;

namespace DiplomaChat.Common.MessageQueueing.MessageQueueing.RabbitMQ
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class RabbitMQConnection : IMessageQueueConnection
    {
        private readonly IConnection _connection;

        public RabbitMQConnection(IConnection connection)
        {
            _connection = connection;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

        public IMessageQueuePublisher CreatePublisher(string queueName)
        {
            var channel = _connection.CreateModel();
            var publisher = new RabbitMQPublisher(channel, queueName);

            return publisher;
        }

        public IMessageQueueReader CreateReader(string queueName)
        {
            var channel = _connection.CreateModel();
            var reader = new RabbitMqReader(channel, queueName);

            return reader;
        }
    }
}