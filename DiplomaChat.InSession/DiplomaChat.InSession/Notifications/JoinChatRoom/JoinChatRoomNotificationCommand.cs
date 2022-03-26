using System;
using MediatR;

namespace TileGameServer.InSession.Notifications.JoinChatRoom
{
    public class JoinChatRoomNotificationCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid ChatRoomId { get; set; }

        public string PlayerNickname { get; set; }
    }
}