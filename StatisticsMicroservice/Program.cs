
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;
using StatisticsMicroservice;
using StatisticsMicroservice.Application.Services;
using StatisticsMicroservice.Application.Services.Interfaces;
using StatisticsMicroservice.Infrastructure.ML;
using StatisticsMicroservice.Infrastructure.RabbitMQ;
using StatisticsMicroservice.Repositories;
using StatisticsMicroservice.Services.Interfaces;
using StatisticsMicroservice.Services.RabbitMQ.Connection;
using UserMicroservice.Enums;
using UserMicroservice.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<StatisticsDbContext>(
    options => {
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

builder.Services.Configure<AuthorizationOptions>(builder.Configuration.GetSection(nameof(AuthorizationOptions)));

builder.Services.RequirePermissions("Create", Permission.Delete);

builder.Services.RequirePermissions("Read", Permission.Read);

builder.Services.RequirePermissions("Update", Permission.Delete);

builder.Services.RequirePermissions("Delete", Permission.Delete);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<ITrainingDataLoader, TrainingDataLoader>();
builder.Services.AddSingleton<IExerciseCategoryIdentifier, ExerciseCategoryIdentifier>();
builder.Services.AddScoped<IStatisticsRepository, StatisticsRepository>();
builder.Services.AddSingleton<IRabbitMqConnection>(new RabbitMqConnection());
builder.Services.AddScoped<IMessageSubscriber, RabbitMqSubscriber>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();
builder.Services.AddHostedService<RabbitMqBackgroundService>();

builder.Services.AddApiAuthentication();

var app = builder.Build();

app.ApplyDatabaseMigrations<StatisticsDbContext>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
