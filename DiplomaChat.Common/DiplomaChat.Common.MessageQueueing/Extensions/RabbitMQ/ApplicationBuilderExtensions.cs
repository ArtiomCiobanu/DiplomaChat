using System;
using System.Linq;
using System.Reflection;
using DiplomaChat.Common.Infrastructure.Reflection;
using DiplomaChat.Common.MessageQueueing.Attributes;
using DiplomaChat.Common.MessageQueueing.MessageQueueing;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace DiplomaChat.Common.MessageQueueing.Extensions.RabbitMQ
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMessageQueueingServices(
            this IApplicationBuilder app,
            Assembly executingAssembly)
        {
            var messageQueueServices = ReflectionHelper
                .GetAllTypesWithAttribute<MessageQueueServiceAttribute>(executingAssembly)
                .ToArray();

            var connection = app.ApplicationServices.GetService<IMessageQueueConnection>();

            foreach (var messageQueueServiceType in messageQueueServices)
            {
                var methodsWithAttribute = messageQueueServiceType
                    .GetMethods()
                    .Where(m => m.GetCustomAttributes<MessageQueueActionAttribute>().Any());

                var messageQueueServiceActionMethods = methodsWithAttribute
                    .ToDictionary(
                        m => m.GetCustomAttribute<MessageQueueActionAttribute>()?.QueueName,
                        m => m);

                foreach (var (queueName, method) in messageQueueServiceActionMethods)
                {
                    var reader = connection?.CreateReader(queueName);

                    var parameter = method.GetParameters().FirstOrDefault();
                    var parameterType = parameter?.ParameterType;

                    reader?.SetReceiveAction(message =>
                    {
                        var serviceInstance = app.ApplicationServices.GetService(messageQueueServiceType);

                        var deserializedMessage = JsonConvert.DeserializeObject(message, parameterType!);

                        method.Invoke(serviceInstance, new[] {deserializedMessage});
                    });
                    reader?.StartReading();
                }
            }

            return app;
        }

        public static IApplicationBuilder UseMessageQueueingServices(
            this IApplicationBuilder app,
            Type assemblyType)
        {
            var executingAssembly = assemblyType.Assembly;

            return app.UseMessageQueueingServices(executingAssembly);
        }
    }
}