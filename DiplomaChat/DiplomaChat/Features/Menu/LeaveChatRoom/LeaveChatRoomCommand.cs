using System;
using DiplomaChat.Common.Infrastructure;
using MediatR;

namespace TileGameServer.Features.Menu.LeaveChatRoom
{
    public class LeaveChatRoomCommand : IQuery<Unit>
    {
        public Guid UserId { get; init; }
        public Guid ChatRoomId { get; init; }
    }
}