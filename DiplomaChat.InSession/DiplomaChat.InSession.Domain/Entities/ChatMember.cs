using DiplomaChat.Common.DataAccess.Entities;

namespace DiplomaChat.InSession.Domain.Entities
{
    public class ChatMember : BaseEntity
    {
        public string Nickname { get; set; }

        public ChatRoom ChatRoom { get; init; }
    }
}