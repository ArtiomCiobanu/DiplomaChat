using DiplomaChat.Common.DataAccess.EntityConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TileGameServer.DataAccess.Entities;

namespace TileGameServer.DataAccess.EntityConfigurations
{
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(player => player.Nickname).IsRequired().HasMaxLength(50);

            builder.HasOne(u => u.ChatRoomUser).WithOne(cru => cru.User).IsRequired();
        }
    }
}