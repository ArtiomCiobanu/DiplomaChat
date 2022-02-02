using System;
using DiplomaChat.Common.Infrastructure.Enums;
using MediatR;

namespace TileGameServer.Features.Menu.Notifications.CreateChatRoom
{
    public class CreateChatRoomNotificationCommand : IRequest
    {
        public ResponseStatus ResponseStatus { get; set; }

        public Guid GameSessionId { get; set; }
    }
}