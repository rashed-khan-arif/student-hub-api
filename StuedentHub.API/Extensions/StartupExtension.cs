using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using StudentHub.Models.Auth;
using StudentHub.Repositories.Core;
using StudentHub.Repositories.Repo;
using StudentHub.Repositories.Repo.Interfaces;
using StudentHub.Supervisor.Impl;
using StudentHub.Supervisor.Interfaces;
using System.Text;

namespace StudentHub.API.Extensions
{
    public static class StartupExtension
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Lockout.MaxFailedAccessAttempts = 10;
            })
              .AddEntityFrameworkStores<StudentHUBDbContext>()
              .AddDefaultTokenProviders();

            var key = Encoding.UTF8.GetBytes(configuration["JWT:Secret"]);
            var sighingkey = new SymmetricSecurityKey(key);

            // Adding Authentication  
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
             {
                 options.SaveToken = true;
                 options.RequireHttpsMetadata = false;
                 options.TokenValidationParameters = new TokenValidationParameters()
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidAudience = configuration["JWT:ValidAudience"],
                     ValidIssuer = configuration["JWT:ValidIssuer"],
                     IssuerSigningKey = sighingkey
                 };
             });

            return services;
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {

            services
                .AddScoped(typeof(IStHubRepository<>), typeof(StHubRepository<>))
                .AddScoped<IStudentService, StudentService>();

            return services;
        }
    }
}
