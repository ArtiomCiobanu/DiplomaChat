using DiplomaChat.Common.Infrastructure.MessageQueueing.Configuration;
using RabbitMQ.Client;

namespace DiplomaChat.Common.Infrastructure.MessageQueueing.MessageQueueing.RabbitMQ
{
    public class RabbitMQConnectionFactory : IMessageQueueConnectionFactory
    {
        private readonly ConnectionFactory _connectionFactory;

        public RabbitMQConnectionFactory(RabbitMQConfiguration rabbitMqConfiguration)
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = rabbitMqConfiguration.HostName,
                Port = rabbitMqConfiguration.Port,
                VirtualHost = rabbitMqConfiguration.VirtualHost,
                UserName = rabbitMqConfiguration.UserName,
                Password = rabbitMqConfiguration.Password
            };
        }

        public IMessageQueueConnection CreateConnection()
        {
            var connection = _connectionFactory.CreateConnection();

            return new RabbitMQConnection(connection);
        }
    }
}