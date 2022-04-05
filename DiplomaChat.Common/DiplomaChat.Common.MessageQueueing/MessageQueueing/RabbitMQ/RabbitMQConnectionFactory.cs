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
                //Uri = new Uri("amqp://guest:guest@localhost:5672")
                //Uri = new Uri("amqp://DiplomaChat:DiplomaChat@rabbit-server:5672")
                /*HostName = rabbitMqConfiguration.HostName,
                Port = rabbitMqConfiguration.Port,
                VirtualHost = rabbitMqConfiguration.VirtualHost,
                UserName = rabbitMqConfiguration.UserName,
                Password = rabbitMqConfiguration.Password*/

                HostName = "rabbit",
                Port = 5672,
                VirtualHost = "/",
                UserName = "guest",
                Password = "guest"
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