using DiplomaChat.Common.Infrastructure.Reflection;
using DiplomaChat.Common.MessageQueueing.Attributes;
using DiplomaChat.Common.MessageQueueing.Configuration;
using DiplomaChat.Common.MessageQueueing.MessageQueueing;
using DiplomaChat.Common.MessageQueueing.MessageQueueing.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace DiplomaChat.Common.MessageQueueing.Extensions.RabbitMQ
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