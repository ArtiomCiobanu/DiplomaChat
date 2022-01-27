using DiplomaChat.SingleSignOn.Features.Accounts.GetAccount;
using FluentValidation;

namespace DiplomaChat.SingleSignOn.Validators
{
    public class GetAccountRequestValidator : AbstractValidator<GetAccountRequest>
    {
        public GetAccountRequestValidator() 
        {
            RuleFor(request => request.AccountId)
                .NotEmpty()
                .NotNull();
        }
    }
}
