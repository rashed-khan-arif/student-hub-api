using EFCoreSecondLevelCacheInterceptor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using StudentHub.API.Extensions;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var isDebug = true;
#if !DEBUG
            isDebug = false;
#endif
// For Identity  
builder.Services.AddIdentity(builder.Configuration);

builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddMvc();

builder.Services.AddEFSecondLevelCache(options =>
            options.UseMemoryCacheProvider()
                    .DisableLogging(!isDebug)
        //.CacheAllQueries(CacheExpirationMode.Sliding, TimeSpan.FromHours(1))

        // Please use the `CacheManager.Core` or `EasyCaching.Redis` for the Redis cache provider.
        );
builder.Services.AddHttpContextAccessor();
builder.Services.AddDatabase();
builder.Services.AddRepositories();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Student HUB API",
        Version = "v1",
        License = new OpenApiLicense
        {
            Name = "Student HUB",
            Url = new Uri("https://student-hub.com")
        },
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Rashed Khan",
            Email = "student.hub.bangladesh@gmail.com",
            Url = new Uri("https://rashedkhan.com/")
        },
        Description = "Student HUB Documentation"
    }); ;
});
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
