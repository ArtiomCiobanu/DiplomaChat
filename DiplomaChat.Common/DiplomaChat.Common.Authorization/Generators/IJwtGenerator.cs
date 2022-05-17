using System.Security.Claims;

namespace DiplomaChat.Common.Infrastructure.Authorization.Generators
{
    public interface IJwtGenerator
    {
        string GenerateToken(params Claim[] claims);
    }
}
