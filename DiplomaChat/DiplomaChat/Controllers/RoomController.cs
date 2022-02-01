﻿using System;
using System.Threading.Tasks;
using DiplomaChat.Common.Authorization.Constants;
using DiplomaChat.Common.Authorization.Extensions;
using DiplomaChat.Common.Infrastructure.Controllers;
using DiplomaChat.Common.Infrastructure.ResponseMappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TileGameServer.Features.Menu.CreateChatRoom;
using TileGameServer.Features.Menu.JoinChatRoom;
using TileGameServer.Features.Menu.LeaveChatRoom;
using TileGameServer.Features.Menu.ListCreatedChatRoom;
using TileGameServer.Features.Menu.Notifications.CreateChatRoom;
using TileGameServer.Features.Menu.Notifications.JoinChatRoom;
using TileGameServer.Features.Menu.Notifications.LeaveChatRoom;

namespace TileGameServer.Controllers
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

        [HttpPost("create")]
        public async Task<IActionResult> CreateGame(
            [FromBody] CreateGameSessionDto dto)
        {
            var command = new CreateChatRoomCommand
            {
                AccountId = AccountId,
                SessionCapacity = dto.SessionCapacity
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