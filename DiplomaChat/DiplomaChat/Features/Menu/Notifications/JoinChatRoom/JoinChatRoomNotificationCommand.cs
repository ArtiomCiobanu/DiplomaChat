using System;
using DiplomaChat.Common.Infrastructure.Enums;
using MediatR;

namespace TileGameServer.Features.Menu.Notifications.JoinChatRoom
{
    public class JoinChatRoomNotificationCommand : IRequest
    {
        public ResponseStatus ResponseStatus { get; set; }

        public Guid UserId { get; set; }
        public Guid ChatRoomId { get; set; }
    }
}