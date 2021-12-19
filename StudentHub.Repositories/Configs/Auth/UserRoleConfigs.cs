using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentHub.Models.Auth;

namespace TopGear.Repos.Configs
{
    public class UserRoleConfigs : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRoles");

            builder.HasData(new UserRole
            {
                RoleId = -1,
                UserId = -1
            });
        }
    }
}