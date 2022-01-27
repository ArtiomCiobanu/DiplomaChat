using System.IdentityModel.Tokens.Jwt;
using DiplomaChat.Common.Authorization.Constants;
using DiplomaChat.Common.Authorization.Extensions;

namespace DiplomaChat.Common.Authorization.Readers
{
    public class JwtReader : ITokenReader
    {
        public string GetAccountId(string token) => GetClaim(token, WebApiClaimTypes.AccountId);

        public string GetClaim(string token, string claimType)
        {
            var jwt = (JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(token);

            var result = jwt.Claims.GetClaim(claimType);
            return result.Value;
        }
    }
}
