using System;
using DiplomaChat.Common.DataAccess.Entities;

namespace DiplomaChat.DataAccess.Entities
{
    public class ChatRoomUser : BaseEntity
    {
        public Guid ChatRoomId { get; set; }
        public Guid UserId { get; set; }
        
        public ChatRoom ChatRoom { get; set; }
        public User User { get; set; }
    }
}