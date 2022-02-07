using System;
using System.Threading.Tasks;
using DiplomaChat.Common.Authorization.Constants;
using DiplomaChat.Common.Authorization.Extensions;
using DiplomaChat.Common.Infrastructure.Controllers;
using DiplomaChat.Common.Infrastructure.ResponseMappers;
using DiplomaChat.Features.Users.GetPlayerProfile;
using DiplomaChat.Features.Users.RegisterPlayer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaChat.Controllers
{
    [Authorize]
    [ApiController]
    [Route("users")]
    public class UserController : BaseMediatorController
    {
        private Guid AccountId => Guid.Parse(User.GetClaim(WebApiClaimTypes.AccountId).Value);

        public UserController(IMediator mediator, IResponseMapper responseMapper)
            : base(mediator, responseMapper)
        {
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterPlayerDto registerPlayerDto)
        {
            var command = new RegisterPlayerCommand
            {
                PlayerId = AccountId,
                PlayerNickname = registerPlayerDto.Nickname
            };

            return await SendCommandToMediatorAsync(command);
        }

        [HttpGet("profile")]
        public Task<IActionResult> GetPlayerProfile()
        {
            var query = new GetPlayerProfileQuery
            {
                UserId = AccountId
            };

            return SendToMediatorAsync(query);
        }
    }
}