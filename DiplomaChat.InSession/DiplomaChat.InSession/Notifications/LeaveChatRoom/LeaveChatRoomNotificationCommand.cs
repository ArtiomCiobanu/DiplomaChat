using MediatR;
using System;

namespace TileGameServer.InSession.Notifications.LeaveGameSession
{
    public class LeaveChatRoomNotificationCommand : IRequest
    {
        public Guid UserId { get; set; }
    }
}