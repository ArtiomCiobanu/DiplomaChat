using System;
using DiplomaChat.Common.Infrastructure;
using MediatR;

namespace DiplomaChat.Features.Menu.JoinChatRoom
{
    public class JoinChatRoomCommand : IQuery<Unit>
    {
        public Guid AccountId { get; init; }
        public Guid RoomId { get; init; }
    }
}
