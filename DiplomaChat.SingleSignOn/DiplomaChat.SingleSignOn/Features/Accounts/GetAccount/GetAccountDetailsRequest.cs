using System;
using DiplomaChat.Common.Infrastructure;

namespace DiplomaChat.SingleSignOn.Features.Accounts.GetAccount
{
    public class GetAccountRequest : IQuery<GetAccountDetailsResponse>
    {
        public Guid AccountId { get; init; }
    }
}