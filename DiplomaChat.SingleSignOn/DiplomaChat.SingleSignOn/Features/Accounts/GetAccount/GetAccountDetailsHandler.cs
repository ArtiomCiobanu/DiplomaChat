using DiplomaChat.Common.DataAccess.Extensions;
using DiplomaChat.Common.Infrastructure.Enums;
using DiplomaChat.Common.Infrastructure.Extensions;
using DiplomaChat.Common.Infrastructure.Responses;
using DiplomaChat.SingleSignOn.DataAccess.Contracts.Context;
using DiplomaChat.SingleSignOn.DataAccess.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiplomaChat.SingleSignOn.Features.Accounts.GetAccount
{
    public class GetAccountDetailsHandler : IRequestHandler<GetAccountRequest, IResponse<GetAccountDetailsResponse>>
    {
        private readonly ISSOContext _ssoContext;

        public GetAccountDetailsHandler(ISSOContext ssoContext)
        {
            _ssoContext = ssoContext;
        }

        public async Task<IResponse<GetAccountDetailsResponse>> Handle(
            GetAccountRequest request,
            CancellationToken cancellationToken)
        {
            var account = await _ssoContext.EntitySet<Account>()
                .Where(a => a.Id == request.AccountId)
                .Select(a => new { a.FirstName, a.LastName })
                .TopOneAsync(cancellationToken);

            if (account == null)
            {
                return new Response<GetAccountDetailsResponse>
                {
                    Status = ResponseStatus.Unauthorized
                };
            }

            var getAccountResponse = new GetAccountDetailsResponse
            {
                FirstName = account.FirstName,
                LastName = account.LastName
            };

            return getAccountResponse.Success();
        }
    }
}