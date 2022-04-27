using MediatR;

namespace TileGameServer.InSession.Notifications.LeaveChatRoom
{
    public class LeaveChatRoomNotificationCommand : IRequest
    {
        public Guid UserId { get; set; }
    }
}