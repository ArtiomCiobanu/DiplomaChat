using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;

namespace DiplomaChat.Common.Authorization.Extensions
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
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(TValue));

            var claim = claims.FirstOrDefault(c => c.Type == claimType);

            TValue? result = claim != null ? (TValue)converter.ConvertFromString(claim.Value) : null;
            return result;
        }
    }
}
