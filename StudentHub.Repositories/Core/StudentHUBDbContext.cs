using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentHub.Models;
using StudentHub.Models.Auth;
using StudentHub.Models.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHub.Repositories.Core
{
    public class StudentHUBDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public StudentHUBDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("Users");
            builder.Entity<UserClaim>().ToTable("UserClaims");
            builder.Entity<UserLogin>().ToTable("UserLogins");
            builder.Entity<UserRefreshToken>().ToTable("UserRefreshTokens");
            builder.Entity<UserRole>().ToTable("UserRoles"); 
            builder.Entity<UserToken>().ToTable("UserTokens");
            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<RoleClaim>().ToTable("RoleClaims");
            builder.Entity<RoleFeature>().ToTable("RoleFeatures");
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly, type => type.Namespace?.Contains("Configs") ?? false);
        }


       // public DbSet<Student> Students { get; set; }
    }
}
