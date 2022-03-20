using System;

namespace DiplomaChat.Features.Rooms.ListCreatedChatRooms
{
    public class ListCreatedChatRoomsResponse
    {
        public ListedGameSession[] GameSessions { get; set; }
    }

    public class ListedGameSession
    {
        public Guid Id { get; set; }
        public string CreatorNickname { get; set; }
        public int UserAmount { get; set; }
    }
}