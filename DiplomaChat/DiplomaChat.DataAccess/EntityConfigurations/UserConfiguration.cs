using DiplomaChat.Common.DataAccess.EntityConfigurations;
using DiplomaChat.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiplomaChat.DataAccess.EntityConfigurations
{
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(player => player.Nickname).IsRequired().HasMaxLength(50);
        }
    }
}