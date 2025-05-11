   
using ApiGateway.Middlewares;
using ApiGateway.Services.RabbitMq;
using ApiGateway.Services.RabbitMq.Connection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot();
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

builder.Services.AddSingleton<IRabbitMqConnection>(new RabbitMqConnection());
builder.Services.AddScoped<IMessageProducer, RabbitMqProducer>();

var app = builder.Build();

app.UseCors(); 

app.UseHttpsRedirection();

app.UseMiddleware<AuthMiddleware>();
app.UseWebSockets();
app.UseOcelot().Wait();

app.Run();
    