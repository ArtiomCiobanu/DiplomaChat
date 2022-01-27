using DiplomaChat.Common.Infrastructure;
using DiplomaChat.Common.Logging.Attributes;

namespace DiplomaChat.SingleSignOn.Features.Accounts.RegisterAccount
{
    [NotLogged]
    public class RegisterAccountCommand : ICommand
    {
        public string Email { get; init; }
        public string Password { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
    }
}