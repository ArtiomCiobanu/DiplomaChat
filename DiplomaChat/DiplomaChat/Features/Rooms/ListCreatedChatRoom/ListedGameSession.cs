using System;

namespace DiplomaChat.Features.Rooms.ListCreatedChatRoom
{
    public class ListedGameSession
    {
        public Guid Id { get; set; }
        public string CreatorNickname { get; set; }
        public int Capacity { get; set; }
        public int PlayerAmount { get; set; }
    }
}