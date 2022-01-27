using System.Linq;
using System.Reflection;
using DiplomaChat.Common.Logging.Attributes;
using DiplomaChat.Common.Logging.Entries;
using DiplomaChat.Common.Logging.EntryLoggers;
using DiplomaChat.Common.Logging.NotLoggedStores.Properties;
using DiplomaChat.Common.Logging.NotLoggedStores.Types;
using DiplomaChat.Common.Logging.Sanitizers.Endpoint;
using DiplomaChat.Common.Logging.Sanitizers.Endpoint.Request;
using DiplomaChat.Common.Logging.Sanitizers.Endpoint.Response;
using DiplomaChat.Common.Logging.Sanitizers.Objects;
using DiplomaChat.Common.Logging.Sanitizers.Objects.Generic;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DiplomaChat.Common.Logging.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLoggers(this IServiceCollection services)
        {
            services.AddScoped<IEntryLogger<EndpointLogEntry>, EndpointEntryLogger>();

            return services;
        }

        public static IServiceCollection AddSanitizing(this IServiceCollection services, Assembly assembly)
        {
            services.AddNotLoggedTypes(assembly).AddNotLoggedProperties(assembly);

            services.AddScoped<IObjectSanitizer, ObjectSanitizer>();
            services.AddScoped(typeof(IObjectSanitizer<>), typeof(ObjectSanitizer<>));

            services.AddScoped(typeof(IRequestSanitizer<>), typeof(RequestSanitizer<>));
            services.AddScoped(typeof(IResponseSanitizer<>), typeof(ResponseSanitizer<>));
            services.AddScoped(typeof(IEndpointSanitizer<,>), typeof(EndpointSanitizer<,>));

            return services;
        }

        public static IServiceCollection AddNotLoggedTypes(this IServiceCollection services, Assembly assembly)
        {
            var notLoggedTypes = assembly.GetTypes()
                .Where(t => t.GetCustomAttribute(typeof(NotLoggedAttribute)) is not null)
                .ToArray();

            var notLoggedRequests = notLoggedTypes.Where(t => t.GetInterfaces().Contains(typeof(IBaseRequest)));
            var notLoggedResponses = notLoggedTypes.Except(notLoggedRequests).Union(new[] { typeof(Unit) });

            services.AddSingleton<INotLoggedTypeStore, NotLoggedTypeStore>(
                _ => new NotLoggedTypeStore(notLoggedRequests, notLoggedResponses));

            return services;
        }

        public static IServiceCollection AddNotLoggedProperties(this IServiceCollection services, Assembly assembly)
        {
            var notLoggedProperties = assembly.GetTypes()
                .SelectMany(t => t.GetProperties().Where(p => p.GetCustomAttribute(typeof(NotLoggedAttribute)) is not null))
                .Select(p => new NotLoggedPropertyInfo
                {
                    Name = p.Name,
                    DeclaringType = p.DeclaringType
                })
                .ToArray();

            services.AddSingleton<INotLoggedPropertyStore, NotLoggedPropertyStore>(
                _ => new NotLoggedPropertyStore(notLoggedProperties));

            return services;
        }
    }
}
