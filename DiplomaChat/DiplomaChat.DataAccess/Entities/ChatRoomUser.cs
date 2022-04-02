using System;

namespace DiplomaChat.DataAccess.Entities
{
    public class ChatRoomUser
    {
        public Guid UserId { get; set; }
        public Guid ChatRoomId { get; set; }
        
        public ChatRoom ChatRoom { get; set; }
        public User User { get; set; }
    }
}