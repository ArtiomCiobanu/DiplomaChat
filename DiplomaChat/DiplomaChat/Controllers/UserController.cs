using System;
using System.Threading.Tasks;
using DiplomaChat.Common.Authorization.Constants;
using DiplomaChat.Common.Authorization.Extensions;
using DiplomaChat.Common.Infrastructure.Controllers;
using DiplomaChat.Common.Infrastructure.ResponseMappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TileGameServer.Features.Users.GetPlayerProfile;
using TileGameServer.Features.Users.RegisterPlayer;

namespace TileGameServer.Controllers
{
    [Authorize]
    [ApiController]
    [Route("players")]
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