using MediatR;
using System;

namespace TileGameServer.InSession.Notifications.CreateChatRoom
{
    public class CreateChatRoomNotificationCommand: IRequest
    {
        public Guid ChatRoomId { get; set; }
    }
}