using DiplomaChat.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiplomaChat.DataAccess.EntityConfigurations
{
    public class ChatRoomUserConfiguration : IEntityTypeConfiguration<ChatRoomUser>
    {
        public void Configure(EntityTypeBuilder<ChatRoomUser> builder)
        {
            builder.HasKey(cru => new { cru.UserId, cru.ChatRoomId });

            builder.Property(cru => cru.ChatRoomId).IsRequired();

            builder.HasOne(cru => cru.ChatRoom);
            builder.HasOne(cru => cru.User);
        }
    }
}