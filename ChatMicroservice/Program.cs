
using ChatMicroservice.API.Hubs;
using ChatMicroservice.Application.Abstractions;
using ChatMicroservice.Application.Services;
using ChatMicroservice.Domain.Abstractions;
using ChatMicroservice.Infrastructure.RabbitMq;
using ChatMicroservice.Infrastructure.Redis;
using ChatMicroservice.Infrastructure.SignalR;
using ChatMicroservice.Persistence;
using ChatMicroservice.Persistence.Repositories;
using UserMicroservice.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStackExchangeRedisCache(options =>
{
    var connection = builder.Configuration.GetConnectionString("Redis");
    options.Configuration = connection;
});

builder.Services.AddSingleton<MongoDbService>();

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
builder.Services.AddScoped<IChatNotifier, ChatNotifier>();
builder.Services.AddScoped<IUserChatRoomCache, UserChatRoomCache>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddSingleton<IMessageProducer, RabbitMqProducer>();
builder.Services.AddSignalR();

builder.Services.AddApiAuthentication();

var app = builder.Build();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ChatHub>("/api/chat");

app.Run();
