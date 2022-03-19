using System;
using System.Threading.Tasks;
using DiplomaChat.Common.Authorization.Constants;
using DiplomaChat.Common.Authorization.Extensions;
using DiplomaChat.Common.Infrastructure.Controllers;
using DiplomaChat.Common.Infrastructure.ResponseMappers;
using DiplomaChat.Features.Rooms.CreateChatRoom;
using DiplomaChat.Features.Rooms.GetChatRoomDetails;
using DiplomaChat.Features.Rooms.JoinChatRoom;
using DiplomaChat.Features.Rooms.LeaveChatRoom;
using DiplomaChat.Features.Rooms.ListCreatedChatRoom;
using DiplomaChat.Features.Rooms.Notifications.CreateChatRoom;
using DiplomaChat.Features.Rooms.Notifications.JoinChatRoom;
using DiplomaChat.Features.Rooms.Notifications.LeaveChatRoom;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiplomaChat.Controllers
{
    [Authorize]
    [ApiController]
    [Route("rooms")]
    public class RoomController : BaseMediatorController
    {
        private Guid AccountId => Guid.Parse(User.GetClaim(WebApiClaimTypes.AccountId).Value);

        public RoomController(IMediator mediator, IResponseMapper responseMapper)
            : base(mediator, responseMapper)
        {
        }

        [HttpGet("create")]
        public async Task<IActionResult> CreateGame()
        {
            var command = new CreateChatRoomCommand
            {
                AccountId = AccountId
            };
            var response = await Mediator.Send(command);

            var createGameNotificationCommand = new CreateChatRoomNotificationCommand
            {
                ResponseStatus = response.Status,
                GameSessionId = response.Result.SessionId
            };
            await Mediator.Send(createGameNotificationCommand);

            return await ResponseMapper.ExecuteAndMapStatusAsync(response);
        }

        [HttpPost("{roomId:guid}/join")]
        public async Task<IActionResult> JoinGame(Guid roomId)
        {
            var command = new JoinChatRoomCommand
            {
                AccountId = AccountId,
                RoomId = roomId
            };
            var response = await Mediator.Send(command);

            var joinGameNotificationCommand = new JoinChatRoomNotificationCommand
            {
                ResponseStatus = response.Status,
                UserId = AccountId,
                ChatRoomId = roomId
            };
            await Mediator.Send(joinGameNotificationCommand);

            return await ResponseMapper.ExecuteAndMapStatusAsync(response);
        }

        [HttpGet("{chatRoomId:guid}/leave")]
        public async Task<IActionResult> Leave(Guid chatRoomId)
        {
            var command = new LeaveChatRoomCommand
            {
                UserId = AccountId,
                ChatRoomId = chatRoomId
            };

            var response = await Mediator.Send(command);

            var leaveGameNotificationCommand = new LeaveChatRoomNotificationCommand
            {
                ResponseStatus = response.Status,
                UserId = AccountId
            };
            await Mediator.Send(leaveGameNotificationCommand);

            return await ResponseMapper.ExecuteAndMapStatusAsync(response);
        }

        [HttpGet("{chatRoomId:guid}/details")]
        public async Task<IActionResult> GetChatRoomDetails(Guid chatRoomId)
        {
            var query = new GetChatRoomDetailsQuery(chatRoomId);

            return await SendToMediatorAsync(query);
        }

        [HttpGet("created/{offset:int?}/{limit:int?}")]
        public async Task<IActionResult>
            ListCreatedGameSessions(int offset = 0, int limit = 10)
        {
            var command = new ListCreatedChatRoomsRequest
            {
                Offset = offset,
                Limit = limit
            };

            return await SendToMediatorAsync(command);
        }
    }
}