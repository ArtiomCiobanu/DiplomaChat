using DiplomaChat.Common.Infrastructure;
using System;

namespace DiplomaChat.Features.Rooms.GetChatRoomDetails
{
    public record class GetChatRoomDetailsQuery(Guid ChatRoomId) : IQuery<GetChatRoomDetailsResponse>;
}
