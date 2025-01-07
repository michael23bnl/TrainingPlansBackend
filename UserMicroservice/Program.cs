using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using UserMicroservice;
using UserMicroservice.Enums;
using UserMicroservice.Extensions;
using UserMicroservice.Infrastructure;
using UserMicroservice.Repositories;
using UserMicroservice.Repositories.Interfaces;
using UserMicroservice.Services;
using AuthorizationOptions = UserMicroservice.Infrastructure.AuthorizationOptions;

var builder = WebApplication.CreateBuilder(args);

var corsPolicy = "corsPolicy";

var corsOptions = builder.Configuration.GetSection(nameof(CorsOptions)).Get<CorsOptions>();

builder.Services.AddCors(options => {
    options.AddPolicy(name: corsPolicy,
        policy => {
            if (corsOptions!.AllowAnyOrigin) {
                policy.AllowAnyOrigin();
            }
            else {
                policy.WithOrigins(corsOptions.AllowedOrigins);
            }
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowCredentials();
        });
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

builder.Services.Configure<AuthorizationOptions>(builder.Configuration.GetSection(nameof(AuthorizationOptions)));

builder.Services.RequirePermissions("Create", Permission.Delete);

builder.Services.RequirePermissions("Read", Permission.Read);

builder.Services.RequirePermissions("Update", Permission.Delete);

builder.Services.RequirePermissions("Delete", Permission.Delete);

builder.Services.AddApiAuthentication();

//builder.Services.AddApiAuthentication(builder.Configuration.GetSection(nameof(JwtOptions)));
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
builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
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
    MinimumSameSitePolicy = SameSiteMode.Lax, // Поддержка кросс-доменных запросов
    HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always,        // Защита от доступа через JS
    Secure = CookieSecurePolicy.SameAsRequest // HTTPS только если запрос HTTPS
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();