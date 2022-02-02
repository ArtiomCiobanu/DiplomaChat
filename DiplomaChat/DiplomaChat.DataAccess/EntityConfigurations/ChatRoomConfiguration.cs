using DiplomaChat.Common.DataAccess.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TileGameServer.DataAccess.Entities;

namespace TileGameServer.DataAccess.EntityConfigurations
{
    public class ChatRoomConfiguration : BaseEntityConfiguration<ChatRoom>
    {
        public override void Configure(EntityTypeBuilder<ChatRoom> builder)
        {
            base.Configure(builder);

            builder.Property(gs => gs.CreationDate).IsRequired();

            builder.HasMany(gs => gs.RoomUsers)
                .WithOne(player => player.ChatRoom).IsRequired();
        }
    }
}