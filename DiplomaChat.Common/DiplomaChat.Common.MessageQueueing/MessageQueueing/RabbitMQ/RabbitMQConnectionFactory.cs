using DiplomaChat.Common.MessageQueueing.Configuration;
using RabbitMQ.Client;
using System;

namespace DiplomaChat.Common.MessageQueueing.MessageQueueing.RabbitMQ
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
            Console.WriteLine("\n We're connected to rabbit;\n");

            return new RabbitMQConnection(connection);
        }
    }
}