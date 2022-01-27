using DiplomaChat.SingleSignOn.Features.Accounts.AuthorizeAccount;
using FluentValidation;

namespace DiplomaChat.SingleSignOn.Validators
{
    public class AuthorizeAccountRequestValidator : AbstractValidator<AuthorizeAccountRequest>
    {
        public AuthorizeAccountRequestValidator() 
        {
            RuleFor(request => request.Email)
                .EmailAddress()
                .NotEmpty()
                .NotNull()
                .MaximumLength(50);

            RuleFor(request => request.Password)
                .MaximumLength(50)
                .NotNull()
                .NotEmpty();
        }
    }
}
