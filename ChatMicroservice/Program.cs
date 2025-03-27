using ChatMicroservice;
using ChatMicroservice.Hubs;
using ChatMicroservice.Repositories;
using ChatMicroservice.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using UserMicroservice.Infrastructure;
using UserMicroservice.Repositories.Interfaces;

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
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); 
    });
});
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IJwtExtractor, JwtExtractor>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSignalR();

var app = builder.Build();

app.UseCors();

app.MapHub<ChatHub>("api/chat");

app.Run();
