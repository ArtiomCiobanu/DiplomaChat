using DiplomaChat.SingleSignOn.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiplomaChat.SingleSignOn.DataAccess.EntityConfigurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.RoleId).IsRequired();

            builder.Property(a => a.Email).IsRequired().HasMaxLength(50);
            builder.Property(a => a.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(a => a.LastName).IsRequired().HasMaxLength(50);

            builder.Property(a => a.PasswordHash).IsRequired().HasMaxLength(100);
        }
    }
}