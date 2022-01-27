using System;
using DiplomaChat.Common.MessageQueueing.Configuration;
using DiplomaChat.Common.MessageQueueing.MessageQueueing;
using DiplomaChat.Common.MessageQueueing.MessageQueueing.RabbitMQ;

namespace MessageQueueingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var rabbitMqConfiguration = new RabbitMQConfiguration()
            {
                HostName = "3.69.44.62",
                Port = 5672,
                VirtualHost = "/tcd",
                UserName = "tcd_sso",
                Password = "tcd_sso_password"
            };
            IMessageQueueConnectionFactory factory = new RabbitMQConnectionFactory(rabbitMqConfiguration);
            var connection = factory.CreateConnection();

            IMessageQueueReader leaveGameReader = connection.CreateReader("LeaveGameQueue");
            leaveGameReader.SetReceiveAction(PlayerLeft);
            leaveGameReader.StartReading();

            IMessageQueueReader joinGameReader = connection.CreateReader("JoinGameQueue");
            joinGameReader.SetReceiveAction(PlayerJoined);
            joinGameReader.StartReading();

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        static void PlayerJoined(string message)
        {
            Console.WriteLine($"Player has joined the game: {message}");
        }

        static void PlayerLeft(string message)
        {
            Console.WriteLine($"Player has left the game: {message}");
        }
    }
}