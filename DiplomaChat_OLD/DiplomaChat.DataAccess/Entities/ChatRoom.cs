using System;
using System.Collections.Generic;
using DiplomaChat.Common.DataAccess.Entities;

namespace DiplomaChat.DataAccess.Entities
{
    public class ChatRoom : BaseEntity
    {
        public Guid CreatorId { get; set; }

        public DateTime CreationDate { get; set; }

        public ICollection<ChatRoomUser> RoomUsers { get; set; }
    }
}