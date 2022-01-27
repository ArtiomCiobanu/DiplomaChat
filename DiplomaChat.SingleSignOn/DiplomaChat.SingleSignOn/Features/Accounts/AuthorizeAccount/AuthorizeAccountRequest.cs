using DiplomaChat.Common.Infrastructure;
using DiplomaChat.Common.Logging.Attributes;

namespace DiplomaChat.SingleSignOn.Features.Accounts.AuthorizeAccount
{
    [NotLogged]
    public class AuthorizeAccountRequest : IQuery<AuthorizeAccountResponse>
    {
        public string Email { get; init; }
        public string Password { get; init; }
    }
}