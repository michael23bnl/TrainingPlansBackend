using ChatMicroservice;
using ChatMicroservice.Hubs;
using ChatMicroservice.Repositories;
using ChatMicroservice.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;
using UserMicroservice.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStackExchangeRedisCache(options =>
{
    var connection = builder.Configuration.GetConnectionString("Redis");
    options.Configuration = connection;
});

builder.Services.AddDbContext<ChatDbContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:7000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); 
    });
});
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSignalR();

builder.Services.AddApiAuthentication();

var app = builder.Build();

app.ApplyDatabaseMigrations<ChatDbContext>();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ChatHub>("/api/chat");

app.Run();
