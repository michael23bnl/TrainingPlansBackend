using Microsoft.EntityFrameworkCore;
using TrainingPlans;
using TrainingPlans.Repositories;
using TrainingPlans.Repositories.Interfaces;
using UserMicroservice.Extensions;
using UserMicroservice.Middlewares;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PlansDbContext>(options => 
    options.UseInMemoryDatabase("InMemoryDb"));

builder.Services.AddScoped<IPlansRepository, PlansRepository>();
builder.Services.AddScoped<IFavoritePlansRepository, FavoritePlansRepository>();
builder.Services.AddScoped<IExercisesRepository, ExercisesRepository>();
builder.Services.AddHttpClient();

builder.Services.AddApiAuthentication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseAuthentication();
app.UseMiddleware<AuthorizationMiddleware>();
app.MapControllers(); 
app.UseHttpsRedirection();
app.Run();

