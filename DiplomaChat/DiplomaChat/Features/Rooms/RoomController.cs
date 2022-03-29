using DiplomaChat.Common.Authorization.Constants;
using DiplomaChat.Common.Authorization.Extensions;
using DiplomaChat.Common.Infrastructure.Controllers;
using DiplomaChat.Common.Infrastructure.ResponseMappers;
using DiplomaChat.Features.Rooms.CreateChatRoom;
using DiplomaChat.Features.Rooms.GetChatRoomDetails;
using DiplomaChat.Features.Rooms.JoinChatRoom;
using DiplomaChat.Features.Rooms.LeaveAllChatRooms;
using DiplomaChat.Features.Rooms.LeaveChatRoom;
using DiplomaChat.Features.Rooms.ListCreatedChatRooms;
using DiplomaChat.Features.Rooms.Notifications.CreateChatRoom;
using DiplomaChat.Features.Rooms.Notifications.JoinChatRoom;
using DiplomaChat.Features.Rooms.Notifications.LeaveAllChatRooms;
using DiplomaChat.Features.Rooms.Notifications.LeaveChatRoom;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DiplomaChat.Features.Rooms
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
        public async Task<IActionResult> CreateChatRoom()
        {
            var command = new CreateChatRoomCommand
            {
                AccountId = AccountId
            };
            var response = await Mediator.Send(command);

            var createGameNotificationCommand = new CreateChatRoomNotificationCommand
            {
                ResponseStatus = response.Status,
                ChatRoomId = response.Result.SessionId
            };
            await Mediator.Send(createGameNotificationCommand);

            return await ResponseMapper.ExecuteAndMapStatusAsync(response);
        }

        [HttpGet("{roomId:guid}/join")]
        public async Task<IActionResult> JoinChatRoom(Guid roomId)
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
        public async Task<IActionResult> LeaveChatRoom(Guid chatRoomId)
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

        [HttpGet("leave-all")]
        public async Task<IActionResult> LeaveAllChatRooms()
        {
            var command = new LeaveAllChatRoomsCommand(AccountId);

            var response = await Mediator.Send(command);

            var leaveAllChatRoomsNotificationHandler = new LeaveAllChatRoomsNotificationCommand
            {
                ResponseStatus = response.Status,
                UserId = AccountId,
                RoomAmount = response.Result.RoomIds.Length
            };
            await Mediator.Send(leaveAllChatRoomsNotificationHandler);

            return await ResponseMapper.ExecuteAndMapStatusAsync(response);
        }

        [HttpGet("{chatRoomId:guid}/details")]
        public async Task<IActionResult> GetChatRoomDetails(Guid chatRoomId)
        {
            var query = new GetChatRoomDetailsQuery(chatRoomId);

            return await SendToMediatorAsync(query);
        }

        [HttpGet("created")]
        public async Task<IActionResult> GetCreatedChatRooms(int offset = 0, int limit = 10)
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