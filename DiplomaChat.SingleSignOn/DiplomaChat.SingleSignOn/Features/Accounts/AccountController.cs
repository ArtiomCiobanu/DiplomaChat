using DiplomaChat.Common.Infrastructure.Authorization.Constants;
using DiplomaChat.Common.Infrastructure.Authorization.Extensions;
using DiplomaChat.Common.Infrastructure.Controllers;
using DiplomaChat.Common.Infrastructure.ResponseMappers;
using DiplomaChat.SingleSignOn.Features.Accounts.AuthorizeAccount;
using DiplomaChat.SingleSignOn.Features.Accounts.GetAccount;
using DiplomaChat.SingleSignOn.Features.Accounts.RegisterAccount;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DiplomaChat.SingleSignOn.Features.Accounts
{
    [ApiController]
    [Route("account")]
    public class AccountController : BaseMediatorController
    {
        public AccountController(IMediator mediator, IResponseMapper responseMapper)
            : base(mediator, responseMapper)
        {
        }

        [HttpPost("register")]
        public Task<IActionResult> Register(
            [Required][FromBody] RegisterAccountCommand registerAccountCommand)
            => SendToMediatorAsync(registerAccountCommand);

        [HttpPost("login")]
        public Task<IActionResult> Login(
            [Required][FromBody] AuthorizeAccountRequest authorizeAccountRequest)
            => SendToMediatorAsync(authorizeAccountRequest);

        [Authorize]
        [HttpGet("details")]
        public Task<IActionResult> GetAccountDetails()
        {
            var userId = Guid.Parse(User.GetClaim(WebApiClaimTypes.AccountId).Value);

            var getAccountRequest = new GetAccountRequest
            {
                AccountId = userId
            };

            return SendToMediatorAsync(getAccountRequest);
        }
    }
}