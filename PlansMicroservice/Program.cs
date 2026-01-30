using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Shared.Auth;
using Shared.Extensions;
using Shared.RabbitMq.Connection;
using TrainingPlans.Application.Abstractions;
using TrainingPlans.Application.Services;
using TrainingPlans.Infrastructure.RabbitMq;
using TrainingPlans.Persistence;
using UserMicroservice.Enums;
using UserMicroservice.Extensions;
using UserMicroservice.Infrastructure;
using TrainingPlans.Persistence.Extensions;
using TrainingPlans.Infrastructure.Extensions;
using TrainingPlans.Persistence.Repositories;
using TrainingPlans.Domain.Abstractions;
using TrainingPlans.Infrastructure.Elasticsearch;
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

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddSingleton<IRabbitMqConnection>(await RabbitMqConnection.InitializeConnection());

builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddScoped<IPlansRepository, PlansRepository>();
builder.Services.AddScoped<IExercisesRepository, ExercisesRepository>();
builder.Services.AddScoped<ICustomPlansRepository, CustomPlansRepository>();
builder.Services.AddScoped<IPlansService, PlansService>();
builder.Services.AddScoped<IExercisesService, ExercisesService>();
builder.Services.AddScoped<ICustomPlansService, CustomPlansService>();
builder.Services.AddScoped<IMessageProducer, RabbitMqProducer>();
builder.Services.AddScoped<IElasticService, ElasticService>();

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
await app.SeedPlansData(app.Lifetime.ApplicationStopping);

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers(); 
app.UseHttpsRedirection();
app.Run();

