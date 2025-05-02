using Microsoft.EntityFrameworkCore;
using TrainingPlans;
using TrainingPlans.Configurations;
using TrainingPlans.Extensions;
using TrainingPlans.Repositories;
using TrainingPlans.Repositories.Interfaces;
using TrainingPlans.Services;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TrainingPlans.Services.Statistics;

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
builder.Services.AddScoped<IStatisticsService, StatisticsService>();
builder.Services.AddScoped<IPlansRepository, PlansRepository>();
builder.Services.AddScoped<IFavoritePlansRepository, FavoritePlansRepository>();
builder.Services.AddScoped<IExercisesRepository, ExercisesRepository>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpClient();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretkeysecretkeysecretkeysecretkeysecretkeysecretkey"))
        };
        options.Events = new JwtBearerEvents {
            OnMessageReceived = context => {
                context.Token = context.Request.Cookies["suchatastycookie"];
                return Task.CompletedTask;
            }
        };
    });

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
//app.UseMiddleware<AuthorizationMiddleware>();
app.MapControllers(); 
app.UseHttpsRedirection();
app.Run();

