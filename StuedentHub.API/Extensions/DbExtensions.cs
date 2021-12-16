using StudentHub.Repositories.Core;
using Microsoft.EntityFrameworkCore;
using EFCoreSecondLevelCacheInterceptor;

namespace StudentHub.API.Extensions
{
    public static class DbExtensions
    {
        const string MigrationAssembly = "StudentHub.Repositories";
        const string DbConnection = "DefaultConnection";
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddDbContextPool<StudentHUBDbContext>((sp, options) =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString(DbConnection);

                options.UseSqlServer(connectionString, (options) =>
                {
                    options.MigrationsAssembly(MigrationAssembly);
                })
                .EnableServiceProviderCaching(true)
                .AddInterceptors(sp.GetRequiredService<SecondLevelCacheInterceptor>()); ;

            }, 32);

            return services;
        }
    }
}
