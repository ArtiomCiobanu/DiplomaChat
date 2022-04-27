using System.Reflection;
using DiplomaChat.Common.Infrastructure.MessageQueueing.Attributes;
using DiplomaChat.Common.Infrastructure.MessageQueueing.Configuration;
using DiplomaChat.Common.Infrastructure.MessageQueueing.MessageQueueing;
using DiplomaChat.Common.Infrastructure.MessageQueueing.MessageQueueing.RabbitMQ;
using DiplomaChat.Common.Infrastructure.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace DiplomaChat.Common.Infrastructure.MessageQueueing.Extensions.RabbitMQ
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRabbitMQ(
            this IServiceCollection services,
            RabbitMQConfiguration rabbitMqConfiguration)
        {
            var connectionFactory = new RabbitMQConnectionFactory(rabbitMqConfiguration);
            var connection = connectionFactory.CreateConnection();

            services.AddSingleton<IMessageQueueConnectionFactory, RabbitMQConnectionFactory>(_ => connectionFactory);
            services.AddSingleton<IMessageQueueConnection, RabbitMQConnection>(_ => (RabbitMQConnection) connection);

            return services;
        }

        public static IServiceCollection AddMessageQueueingServices(
            this IServiceCollection services,
            Assembly executingAssembly)
        {
            var messageQueueServices = ReflectionHelper
                .GetAllTypesWithAttribute<MessageQueueServiceAttribute>(executingAssembly);

            foreach (var mqService in messageQueueServices)
            {
                services.AddSingleton(mqService);
            }

            return services;
        }

        public static IServiceCollection AddMessageQueueingServices(
            this IServiceCollection services,
            Type assemblyType)
        {
            var executingAssembly = assemblyType.Assembly;

            return services.AddMessageQueueingServices(executingAssembly);
        }
    }
}