using DiplomaChat.Common.DataAccess.Extensions;
using DiplomaChat.Common.Infrastructure.Authorization.Enums;
using DiplomaChat.Common.Infrastructure.Enums;
using DiplomaChat.Common.Infrastructure.Generators.Hashing;
using DiplomaChat.Common.Infrastructure.Responses;
using DiplomaChat.SingleSignOn.DataAccess.Contracts.Context;
using DiplomaChat.SingleSignOn.DataAccess.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiplomaChat.SingleSignOn.Features.Accounts.RegisterAccount
{
    public class RegisterAccountCommandHandler :
        IRequestHandler<RegisterAccountCommand, IResponse<Unit>>
    {
        private readonly ISSOContext _ssoContext;
        private readonly IHashGenerator _hashGenerator;

        public RegisterAccountCommandHandler(
            ISSOContext ssoContext,
            IHashGenerator hashGenerator)
        {
            _ssoContext = ssoContext;
            _hashGenerator = hashGenerator;
        }

        public async Task<IResponse<Unit>> Handle(
            RegisterAccountCommand request,
            CancellationToken cancellationToken)
        {
            var accountEntitySet = _ssoContext.EntitySet<Account>();

            var foundAccount = await accountEntitySet
                .Where(a => a.Email == request.Email)
                .Select(a => new { a.Id })
                .TopOneAsync(cancellationToken);

            if (foundAccount != null)
            {
                return new Response<Unit>
                {
                    Message = "An account with this email already exists",
                    Status = ResponseStatus.Conflict
                };
            }

            var passwordHash = _hashGenerator.GenerateHash(request.Password);

            var account = new Account
            {
                Id = new Guid(),
                RoleId = (int)UserRole.User,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PasswordHash = passwordHash
            };

            await accountEntitySet.AddAsync(account, cancellationToken);
            await _ssoContext.SaveChangesAsync(cancellationToken);

            return new Response<Unit>
            {
                Message = "Successfully registered",
                Status = ResponseStatus.Success
            };
        }
    }
}