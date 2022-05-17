using DiplomaChat.Common.Infrastructure.Extensions;
using System.Security.Claims;

namespace DiplomaChat.Common.Infrastructure.Authorization.Extensions
{
    public static class ClaimExtensions
    {
        public static Claim GetClaim(this ClaimsPrincipal principal, string claimType)
        {
            return principal.Claims.GetClaim(claimType);
        }

        public static Claim GetClaim(this IEnumerable<Claim> claims, string claimType)
        {
            return claims.First(c => c.Type == claimType);
        }

        public static TValue? GetClaimValueIfExists<TValue>(this IEnumerable<Claim> claims, string claimType)
            where TValue : struct
        {
            var claim = claims.FirstOrDefault(c => c.Type == claimType);

            return claim?.Value.ConvertTo<TValue>();
        }
    }
}
