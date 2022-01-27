using DiplomaChat.Common.Authorization.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace DiplomaChat.Common.Authorization.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddJwtBearerWithConfiguration(
            this IServiceCollection services,
            JwtConfiguration jwtConfiguration,
            SymmetricSecurityKey symmetricSecurityKey)
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtConfiguration.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtConfiguration.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = symmetricSecurityKey,
                ValidateLifetime = jwtConfiguration.ValidateLifetime,
                RequireExpirationTime = jwtConfiguration.RequireExpirationTime,
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = validationParameters;
            });
        }
    }
}
