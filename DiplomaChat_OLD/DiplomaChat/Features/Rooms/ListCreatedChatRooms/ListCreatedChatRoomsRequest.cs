using DiplomaChat.Common.Infrastructure;

namespace DiplomaChat.Features.Rooms.ListCreatedChatRooms
{
    public class ListCreatedChatRoomsRequest : IQuery<ListCreatedChatRoomsResponse>
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}