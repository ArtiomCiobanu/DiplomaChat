using System;
using DiplomaChat.Common.Infrastructure.Enums;
using MediatR;

namespace TileGameServer.Features.Menu.Notifications.LeaveChatRoom
{
    public class LeaveChatRoomNotificationCommand : IRequest
    {
        public ResponseStatus ResponseStatus { get; set; }

        public Guid UserId { get; set; }
    }
}