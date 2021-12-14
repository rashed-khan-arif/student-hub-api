using StudentHub.Repositories.Core;
using Microsoft.EntityFrameworkCore;

namespace StudentHub.API.Extensions
{
    public static class DbExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddDbContextPool<StudentHUBDbContext>((sp, options) =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
               
                //options.UseSqlServer(connectionString, (options) =>
                //{
                //    options.MigrationsAssembly("StudentHub.Repositories.Migrations");
                //});

                options.UseInMemoryDatabase("StudentHUB");
            }, 32);

            return services;
        }
    }
}
