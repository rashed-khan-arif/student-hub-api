using System;
using System.Collections.Generic;
using System.Globalization; 
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentHub.Models.Auth;
using StudentHub.Repositories.Extensions;

namespace StudentHub.Repositories.Configs
{
    public class UserConfigs : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.Property(a => a.FirstName).IsRequired();
            builder.Property(a => a.LastName).IsRequired();
            builder.Property(a => a.Sex).IsRequired();            
            builder.Property(a => a.CreateDate).IsCreateDate().IsRequired();
            builder.Ignore(a => a.Password);
            builder.Ignore(a => a.RoleName);

            builder.HasData(new User
                {
                    Id = -1,
                    UserName = "admin@gmail.com",
                    NormalizedUserName = "ADMIN@GMAIL.COM",
                    Email = "admin@gmail.com",
                    NormalizedEmail = "ADMIN@GMAIL.COM",
                    EmailConfirmed = false,
                    SecurityStamp = "3b3f11a0-7018-472b-8247-144825ac9bca",
                    ConcurrencyStamp = "7bdbcaee-ec8e-49da-8f49-822a783513db",
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    FirstName = "Student",
                    LastName = "Hub",
                    Sex = "M"
            }
            );
        }
    }
}