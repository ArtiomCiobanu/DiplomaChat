using MediatR;
using System;

namespace DiplomaChat.InSession.Notifications.CreateChatRoom
{
    public class CreateChatRoomNotificationCommand: IRequest
    {
        public Guid ChatRoomId { get; set; }
    }
}