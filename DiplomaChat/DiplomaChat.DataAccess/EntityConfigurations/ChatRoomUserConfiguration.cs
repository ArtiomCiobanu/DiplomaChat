using DiplomaChat.Common.DataAccess.EntityConfigurations;
using DiplomaChat.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiplomaChat.DataAccess.EntityConfigurations
{
    public class ChatRoomUserConfiguration : BaseEntityConfiguration<ChatRoomUser>
    {
        public override void Configure(EntityTypeBuilder<ChatRoomUser> builder)
        {
            base.Configure(builder);

            builder.Property(sessionPlayer => sessionPlayer.ChatRoomId).IsRequired();

            builder.HasOne(sessionPlayer => sessionPlayer.ChatRoom)
                .WithMany(gs => gs.RoomUsers).IsRequired();
        }
    }
}