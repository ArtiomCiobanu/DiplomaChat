using DiplomaChat.Common.DataAccess.Entities;

namespace DiplomaChat.DataAccess.Entities
{
    public class User : BaseEntity
    {
        public string Nickname { get; set; }
        
        public ChatRoomUser ChatRoomUser { get; set; }
    }
}