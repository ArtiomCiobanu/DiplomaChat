using System;
using DiplomaChat.Common.Infrastructure;

namespace TileGameServer.Features.Menu.CreateChatRoom
{
    public class CreateChatRoomCommand : IQuery<CreateChatRoomResponse>
    {
        public Guid AccountId { get; init; }
        public int SessionCapacity { get; init; }
    }
}
