using DiplomaChat.Common.Infrastructure.Logging.Attributes;

namespace DiplomaChat.SingleSignOn.Features.Accounts.AuthorizeAccount
{
    [NotLogged]
    public class AuthorizeAccountResponse
    {
        public string Token { get; }

        public AuthorizeAccountResponse(string token)
        {
            Token = token;
        }
    }
}