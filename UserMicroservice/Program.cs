
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;
using Shared.RabbitMq.Connection;
using UserMicroservice;
using UserMicroservice.Entities;
using UserMicroservice.Enums;
using UserMicroservice.Infrastructure;
using UserMicroservice.Repositories;
using UserMicroservice.Repositories.Interfaces;
using UserMicroservice.Services;
using UserMicroservice.Services.RabbitMq;

var builder = WebApplication.CreateBuilder(args);

var corsPolicy = "corsPolicy";

builder.Services.AddCors(options => {
    options.AddPolicy(name: corsPolicy,
        policy => {
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowCredentials();
        });
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

builder.Services.AddDbContext<UserDbContext>(
    options => {
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IJwtExtractor, JwtExtractor>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddSingleton<IRabbitMqConnection>(await RabbitMqConnection.InitializeConnection());
builder.Services.AddScoped<IMessageSubscriber, RabbitMqSubscriber>();
builder.Services.AddHostedService<RabbitMqBackgroundService>();

var app = builder.Build();

app.ApplyDatabaseMigrations<UserDbContext>();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var dbContext = services.GetRequiredService<UserDbContext>();
    var passwordHasher = services.GetRequiredService<IPasswordHasher>();
    var userRepository = services.GetRequiredService<IUsersRepository>();
    
    if (await userRepository.GetByEmail("Admin") == null)
    {
        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            UserName = "Admin",
            Email = "Admin",
            PasswordHash = passwordHasher.Generate("Admin"),
            Roles =
            [
                await dbContext.Roles
                    .SingleOrDefaultAsync(r => r.Id == (int)Role.Admin)
            ]
        };
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
    }
}

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(corsPolicy);

/*app.UseCookiePolicy(new CookiePolicyOptions {
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});*/

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Lax, // поддержка кросс-доменных запросов
    HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always,        // защита от доступа через JS
    Secure = CookieSecurePolicy.SameAsRequest // HTTPS только если запрос HTTPS
});

app.MapControllers();

app.Run();