using DiplomaChat.Common.Authorization.Configuration;
using DiplomaChat.Common.Authorization.Extensions;
using DiplomaChat.Common.Authorization.Generators;
using DiplomaChat.SingleSignOn.RequestPipelines;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DiplomaChat.SingleSignOn.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddJwt(
           this IServiceCollection services,
           JwtConfiguration jwtConfiguration)
        {
            var secretKey = Encoding.UTF8.GetBytes(jwtConfiguration.SecretKey);
            var symmetricSecurityKey = new SymmetricSecurityKey(secretKey);
            services.AddJwtBearerWithConfiguration(jwtConfiguration, symmetricSecurityKey);

            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            services.AddScoped<IJwtGenerator, JwtGenerator>(s => new JwtGenerator(jwtConfiguration, signingCredentials));
        }

        public static IServiceCollection AddLoggingPipeline(this IServiceCollection services)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(EndpointLoggingPipeline<,>));

            return services;
        }
    }
}
