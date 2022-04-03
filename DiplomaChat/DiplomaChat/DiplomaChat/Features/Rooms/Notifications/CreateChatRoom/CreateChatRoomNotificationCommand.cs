using System;
using DiplomaChat.Common.Infrastructure.Enums;
using MediatR;

namespace DiplomaChat.Features.Rooms.Notifications.CreateChatRoom
{
    public class CreateChatRoomNotificationCommand : IRequest
    {
        public ResponseStatus ResponseStatus { get; set; }

        public Guid ChatRoomId { get; set; }
    }
}