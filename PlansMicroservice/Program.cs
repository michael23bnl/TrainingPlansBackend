using Microsoft.EntityFrameworkCore;
using TrainingPlans;
using TrainingPlans.Configurations;
using TrainingPlans.Extensions;
using Microsoft.AspNetCore.Authorization;
using Shared.Extensions;
using TrainingPlans.Application.Services;
using TrainingPlans.Application.Services.Interfaces;
using TrainingPlans.Infrastructure.Elasticsearch;
using TrainingPlans.Infrastructure.RabbitMq;
using TrainingPlans.Infrastructure.RabbitMq.Connection;
using UserMicroservice.Enums;
using UserMicroservice.Extensions;
using UserMicroservice.Infrastructure;
using TrainingPlans.Persistence.Extensions;
using TrainingPlans.Persistence.Repositories;
using TrainingPlans.Persistence.Repositories.Interfaces;
using AuthorizationOptions = UserMicroservice.Infrastructure.AuthorizationOptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PlansDbContext>(
    options => {
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

builder.Services.Configure<AuthorizationOptions>(builder.Configuration.GetSection(nameof(AuthorizationOptions)));

builder.Services.RequirePermissions("Create", Permission.Delete);

builder.Services.RequirePermissions("Read", Permission.Read);

builder.Services.RequirePermissions("Update", Permission.Delete);

builder.Services.RequirePermissions("Delete", Permission.Delete);

builder.Services.Configure<ElasticSettings>(builder.Configuration.GetSection("ElasticSettings"));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddScoped<IPlansSearch, ElasticService>();
builder.Services.AddScoped<IPlansService, PlansService>();
builder.Services.AddScoped<IFavoritePlansService, FavoritePlansService>();
builder.Services.AddScoped<ICompletedPlansService, CompletedPlansService>();
builder.Services.AddScoped<IExercisesService, ExercisesService>();
builder.Services.AddScoped<IPlansRepository, PlansRepository>();
builder.Services.AddScoped<IFavoritePlansRepository, FavoritePlansRepository>();
builder.Services.AddScoped<ICompletedPlansRepository, CompletedPlansRepository>();
builder.Services.AddScoped<IExercisesRepository, ExercisesRepository>();

builder.Services.AddSingleton<IRabbitMqConnection>(new RabbitMqConnection());
builder.Services.AddScoped<IMessageProducer, RabbitMqProducer>();
builder.Services.AddScoped<ICompletedPlansService, CompletedPlansService>();

builder.Services.AddApiAuthentication();

var app = builder.Build();

app.ApplyDatabaseMigrations<PlansDbContext>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.SeedExercisesData(Path.Combine(Directory.GetCurrentDirectory(), "Persistence/Data", "Exercises.json"));
app.SeedPlansData(Path.Combine(Directory.GetCurrentDirectory(), "Persistence/Data", "Plans.json"));
await app.SeedPlansData();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers(); 
app.UseHttpsRedirection();
app.Run();

