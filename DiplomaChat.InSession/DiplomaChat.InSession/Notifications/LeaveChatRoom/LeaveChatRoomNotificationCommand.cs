using MediatR;
using System;

namespace DiplomaChat.InSession.Notifications.LeaveGameSession
{
    public class LeaveChatRoomNotificationCommand : IRequest
    {
        public Guid UserId { get; set; }
    }
}