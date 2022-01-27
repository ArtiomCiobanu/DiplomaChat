using System.Security.Claims;

namespace DiplomaChat.Common.Authorization.Generators
{
    public interface IJwtGenerator
    {
        string GenerateToken(params Claim[] claims);
    }
}
