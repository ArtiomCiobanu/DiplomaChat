namespace DiplomaChat.Common.Infrastructure.Authorization.Configuration
{
    public class JwtConfiguration
    {
        public string Issuer { get; init; }
        public string Audience { get; init; }
        public int TokenLifetime { get; init; }
        public string SecretKey { get; init; }

        public bool ValidateLifetime { get; init; }
        public bool RequireExpirationTime { get; init; }
    }
}
