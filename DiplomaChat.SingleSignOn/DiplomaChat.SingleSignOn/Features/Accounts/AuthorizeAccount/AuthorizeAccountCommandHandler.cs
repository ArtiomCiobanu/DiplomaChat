using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using DiplomaChat.Common.Authorization.Constants;
using DiplomaChat.Common.Authorization.Generators;
using DiplomaChat.Common.DataAccess.Extensions;
using DiplomaChat.Common.Infrastructure.Enums;
using DiplomaChat.Common.Infrastructure.Generators.Hashing;
using DiplomaChat.Common.Infrastructure.Responses;
using DiplomaChat.SingleSignOn.DataAccess.Context;
using DiplomaChat.SingleSignOn.DataAccess.Entities;
using MediatR;

namespace DiplomaChat.SingleSignOn.Features.Accounts.AuthorizeAccount
{
    public class AuthorizeAccountCommandHandler :
        IRequestHandler<AuthorizeAccountRequest, IResponse<AuthorizeAccountResponse>>
    {
        private readonly ISSOContext _ssoContext;
        private readonly IHashGenerator _hashGenerator;
        private readonly IJwtGenerator _jwtGenerator;

        public AuthorizeAccountCommandHandler(
            ISSOContext ssoContext,
            IHashGenerator hashGenerator,
            IJwtGenerator jwtGenerator)
        {
            _ssoContext = ssoContext;
            _hashGenerator = hashGenerator;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<IResponse<AuthorizeAccountResponse>> Handle(
            AuthorizeAccountRequest request,
            CancellationToken cancellationToken)
        {
            var passwordHash = _hashGenerator.GenerateHash(request.Password);

            var account = await _ssoContext.EntitySet<Account>()
                .Where(a => a.PasswordHash.SequenceEqual(passwordHash))
                .Select(a => new {a.Id, a.RoleId})
                .TopOneAsync(cancellationToken);

            if (account == null || account.Id == default)
            {
                return new Response<AuthorizeAccountResponse>
                {
                    Message = "Invalid login credentials",
                    Status = ResponseStatus.Conflict
                };
            }

            var token = _jwtGenerator.GenerateToken(new Claim(WebApiClaimTypes.AccountId, account.Id.ToString()));

            return new Response<AuthorizeAccountResponse>
            {
                Result = new AuthorizeAccountResponse(token),
                Status = ResponseStatus.Success
            };
        }
    }
}