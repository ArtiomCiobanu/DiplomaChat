using System;
using DiplomaChat.Common.Infrastructure;

namespace DiplomaChat.Features.Rooms.CreateChatRoom
{
    public class CreateChatRoomCommand : IQuery<CreateChatRoomResponse>
    {
        public Guid AccountId { get; init; }
    }
}
