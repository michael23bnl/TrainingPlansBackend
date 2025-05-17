using Microsoft.EntityFrameworkCore;
using TrainingPlans;
using TrainingPlans.Configurations;
using TrainingPlans.Extensions;
using TrainingPlans.Repositories;
using TrainingPlans.Repositories.Interfaces;
using TrainingPlans.Services;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using TrainingPlans.Application.Services;
using TrainingPlans.Infrastructure.RabbitMq;
using TrainingPlans.Infrastructure.RabbitMq.Connection;
using UserMicroservice.Enums;
using UserMicroservice.Extensions;
using UserMicroservice.Infrastructure;
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

builder.Services.AddSingleton<IElasticService, ElasticService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddScoped<IPlansRepository, PlansRepository>();
builder.Services.AddScoped<IFavoritePlansRepository, FavoritePlansRepository>();
builder.Services.AddScoped<ICompletedPlansRepository, CompletedPlansRepository>();
builder.Services.AddScoped<IExercisesRepository, ExercisesRepository>();

builder.Services.AddSingleton<IRabbitMqConnection>(new RabbitMqConnection());
builder.Services.AddScoped<IMessageProducer, RabbitMqProducer>();
builder.Services.AddScoped<ICompletedPlansService, CompletedPlansService>();

builder.Services.AddApiAuthentication();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<PlansDbContext>();
        dbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while applying migrations.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.SeedExercisesData(Path.Combine(Directory.GetCurrentDirectory(), "Data", "Exercises.json"));
app.SeedPlansData(Path.Combine(Directory.GetCurrentDirectory(), "Data", "Plans.json"));

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers(); 
app.UseHttpsRedirection();
app.Run();

