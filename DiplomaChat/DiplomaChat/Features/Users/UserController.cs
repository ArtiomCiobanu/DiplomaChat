using DiplomaChat.Common.Infrastructure.Authorization.Constants;
using DiplomaChat.Common.Infrastructure.Authorization.Extensions;
using DiplomaChat.Common.Infrastructure.Controllers;
using DiplomaChat.Common.Infrastructure.ResponseMappers;
using DiplomaChat.Features.Users.GetUserProfile;
using DiplomaChat.Features.Users.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaChat.Features.Users
{
    [Authorize]
    [ApiController]
    [Route("user")]
    public class UserController : BaseMediatorController
    {
        private Guid AccountId => Guid.Parse(User.GetClaim(WebApiClaimTypes.AccountId).Value);

        public UserController(IMediator mediator, IResponseMapper responseMapper)
            : base(mediator, responseMapper)
        {
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerPlayerDto)
        {
            var command = new RegisterUserCommand
            {
                PlayerId = AccountId,
                PlayerNickname = registerPlayerDto.Nickname
            };

            return await SendCommandToMediatorAsync(command);
        }

        [HttpGet("profile")]
        public Task<IActionResult> GetUserProfile() => GetUserProfile(AccountId);

        [HttpGet("{userId}/profile")]
        public Task<IActionResult> GetUserProfile(Guid userId)
        {
            var query = new GetUserProfileQuery
            {
                UserId = userId
            };

            return SendToMediatorAsync(query);
        }
    }
}