using DiplomaChat.Constants;
using DiplomaChat.Domain.Models.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiplomaChat.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddSingletonSessionCapacityConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            var sessionCapacityConfiguration = configuration
                .GetSection(AppSettings.SessionCapacityConfiguration)
                .Get<SessionCapacityConfiguration>();

            return services.AddSingleton(sessionCapacityConfiguration);
        }
    }
}