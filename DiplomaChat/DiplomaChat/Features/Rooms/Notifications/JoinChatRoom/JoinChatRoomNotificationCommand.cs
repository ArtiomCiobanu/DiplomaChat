using System;
using DiplomaChat.Common.Infrastructure.Enums;
using MediatR;

namespace DiplomaChat.Features.Rooms.Notifications.JoinChatRoom
{
    public class JoinChatRoomNotificationCommand : IRequest
    {
        public ResponseStatus ResponseStatus { get; set; }

        public Guid UserId { get; set; }
        public Guid ChatRoomId { get; set; }
    }
}