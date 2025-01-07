using Microsoft.EntityFrameworkCore;
using TrainingPlans;
using TrainingPlans.Extensions;
using TrainingPlans.Repositories;
using TrainingPlans.Repositories.Interfaces;
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

builder.Services.AddScoped<IPlansRepository, PlansRepository>();
builder.Services.AddScoped<IFavoritePlansRepository, FavoritePlansRepository>();
builder.Services.AddScoped<IExercisesRepository, ExercisesRepository>();
builder.Services.AddScoped<IJwtExtractor, JwtExtractor>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpClient();

builder.Services.AddApiAuthentication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.SeedExercisesData();
app.SeedPlansData();
app.UseRouting();
app.UseAuthentication();
app.UseMiddleware<AuthorizationMiddleware>();
app.MapControllers(); 
app.UseHttpsRedirection();
app.Run();

