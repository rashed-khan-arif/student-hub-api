using EFCoreSecondLevelCacheInterceptor;
using StudentHub.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

var isDebug = true;
#if !DEBUG
            isDebug = false;
#endif
// For Identity  
builder.Services.AddIdentity(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEFSecondLevelCache(options =>
            options.UseMemoryCacheProvider()
                    .DisableLogging(!isDebug)
        //.CacheAllQueries(CacheExpirationMode.Sliding, TimeSpan.FromHours(1))

        // Please use the `CacheManager.Core` or `EasyCaching.Redis` for the Redis cache provider.
        );
builder.Services.AddHttpContextAccessor();
builder.Services.AddDatabase();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddResponseCompression();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
