using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DiplomaChat.Common.Authorization.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DiplomaChat.Common.Authorization.Generators
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly SigningCredentials _signingCredentials;

        public JwtGenerator(JwtConfiguration jwtConfiguration, SigningCredentials signingCredentials)
        {
            _jwtConfiguration = jwtConfiguration;
            _signingCredentials = signingCredentials;
        }

        public string GenerateToken(params Claim[] claims)
        {
            var currentDateTime = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                _jwtConfiguration.Issuer,
                _jwtConfiguration.Audience,
                claims,
                currentDateTime,
                currentDateTime.AddDays(_jwtConfiguration.TokenLifetime),
                _signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
