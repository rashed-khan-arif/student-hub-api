using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentHub.Models.Auth;

namespace StudentHub.Repositories.Configs
{
    public class UserRefreshTokenConfigs : IEntityTypeConfiguration<UserRefreshToken>
    {
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Token).IsRequired();
            builder.Property(a => a.ModifiedDate).IsRequired();
            builder.HasOne(a => a.User).WithMany(a => a.UserRefreshTokens).HasForeignKey(a => a.UserId).IsRequired();
            builder.Property(a => a.CreateDate).IsRequired();
            builder.ToTable("UserRefreshTokens");
        }
    }
}