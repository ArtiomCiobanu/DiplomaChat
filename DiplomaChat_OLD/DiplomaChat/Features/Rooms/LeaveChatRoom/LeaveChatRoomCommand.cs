using System;
using DiplomaChat.Common.Infrastructure;
using MediatR;

namespace DiplomaChat.Features.Rooms.LeaveChatRoom
{
    public class LeaveChatRoomCommand : IQuery<Unit>
    {
        public Guid UserId { get; init; }
        public Guid ChatRoomId { get; init; }
    }
}