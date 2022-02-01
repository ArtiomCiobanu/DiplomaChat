using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TileGameServer.DataAccess.Entities;

namespace TileGameServer.DataAccess.EntityConfigurations
{
    public class SessionPlayerConfiguration : BaseEntityConfiguration<ChatRoomUser>
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