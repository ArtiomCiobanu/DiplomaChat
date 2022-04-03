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
                Uri = new Uri("amqp://guest:guest@localhost:5672")
                //Uri = new Uri("amqp://DiplomaChat:DiplomaChat@rabbit-server:5672")
                /*HostName = rabbitMqConfiguration.HostName,
                Port = rabbitMqConfiguration.Port,
                VirtualHost = rabbitMqConfiguration.VirtualHost,
                UserName = rabbitMqConfiguration.UserName,
                Password = rabbitMqConfiguration.Password*/
            };
        }

        public IMessageQueueConnection CreateConnection()
        {
            var connection = _connectionFactory.CreateConnection();

            return new RabbitMQConnection(connection);
        }
    }
}