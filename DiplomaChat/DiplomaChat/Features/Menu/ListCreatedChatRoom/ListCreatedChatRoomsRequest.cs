using DiplomaChat.Common.Infrastructure;

namespace TileGameServer.Features.Menu.ListCreatedChatRoom
{
    public class ListCreatedChatRoomsRequest : IQuery<ListCreatedChatRoomsResponse>
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}