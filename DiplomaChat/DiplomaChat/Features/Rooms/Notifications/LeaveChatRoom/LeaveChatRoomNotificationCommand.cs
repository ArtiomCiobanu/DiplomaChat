using DiplomaChat.Common.Infrastructure.Enums;
using MediatR;

namespace DiplomaChat.Features.Rooms.Notifications.LeaveChatRoom
{
    public class LeaveChatRoomNotificationCommand : IRequest
    {
        public ResponseStatus ResponseStatus { get; set; }

        public Guid UserId { get; set; }
    }
}