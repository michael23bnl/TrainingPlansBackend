using Microsoft.EntityFrameworkCore;
using TrainingPlans;
using TrainingPlans.Configurations;
using TrainingPlans.Extensions;
using TrainingPlans.Repositories;
using TrainingPlans.Repositories.Interfaces;
using TrainingPlans.Services;
using UserMicroservice.Extensions;
using UserMicroservice.Infrastructure;
using UserMicroservice.Middlewares;
using UserMicroservice.Repositories.Interfaces;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PlansDbContext>(
    options => {
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

builder.Services.Configure<ElasticSettings>(builder.Configuration.GetSection("ElasticSettings"));
builder.Services.AddSingleton<IElasticService, ElasticService>();

builder.Services.AddScoped<IPlansRepository, PlansRepository>();
builder.Services.AddScoped<IFavoritePlansRepository, FavoritePlansRepository>();
builder.Services.AddScoped<IExercisesRepository, ExercisesRepository>();
builder.Services.AddScoped<IJwtExtractor, JwtExtractor>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpClient();

builder.Services.AddApiAuthentication();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<PlansDbContext>();
        dbContext.Database.Migrate(); // Применяет все pending миграции
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
app.UseMiddleware<AuthorizationMiddleware>();
app.MapControllers(); 
app.UseHttpsRedirection();
app.Run();

