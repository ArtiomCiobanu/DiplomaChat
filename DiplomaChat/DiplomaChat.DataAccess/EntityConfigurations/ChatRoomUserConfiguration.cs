using DiplomaChat.Common.DataAccess.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TileGameServer.DataAccess.Entities;

namespace TileGameServer.DataAccess.EntityConfigurations
{
    public class ChatRoomUserConfiguration : BaseEntityConfiguration<ChatRoomUser>
    {
        public override void Configure(EntityTypeBuilder<ChatRoomUser> builder)
        {
            base.Configure(builder);

            builder.Property(sessionPlayer => sessionPlayer.ChatRoomId).IsRequired();

            builder.HasOne(sessionPlayer => sessionPlayer.ChatRoom)
                .WithMany(gs => gs.RoomUsers).IsRequired();
            builder.HasOne(cru => cru.User).WithOne(u => u.ChatRoomUser).IsRequired();
        }
    }
}