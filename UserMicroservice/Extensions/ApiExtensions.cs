using System.Text;
using Microsoft.AspNetCore.Authorization;
using UserMicroservice.Enums;
using UserMicroservice.Infrastructure;
using UserMicroservice.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace UserMicroservice.Extensions;

public static class ApiExtensions
{
    /*public static void AddMappedEndpoints(this IEndpointRouteBuilder app) {
        app.MapUsersEndpoints();
    }*/

    private static string secretKey = "secretkeysecretkeysecretkeysecretkeysecretkeysecretkey";

    // настройка аутентификации

    public static void AddApiAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["suchatastycookie"];
                        return Task.CompletedTask;
                    }
                };
            });
        services.AddAuthorization();
    }

    /*public static void AddApiAuthentication(this IServiceCollection services, IConfiguration configuration) {
        var jwtOptions = configuration.Get<JwtOptions>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => {
                options.TokenValidationParameters = new() {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                };
                options.Events = new JwtBearerEvents {
                    OnMessageReceived = context => {
                        context.Token = context.Request.Cookies["suchatastycookie"];
                        return Task.CompletedTask;
                    }
                };
            });
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddAuthorization();
    }*/

    // добавление требований для MinimalAPI
    
    /*public static IEndpointConventionBuilder RequirePermissions<TBuilder>(
        this TBuilder builder, params Permission[] permissions)
        where TBuilder : IEndpointConventionBuilder {
        return builder.RequireAuthorization(policy =>
        policy.AddRequirements(new PermissionRequirenment(permissions)));
    }*/

    // добавление требований для контроллеров

    public static void RequirePermissions(this IServiceCollection services,
        string policyName, params Permission[] permissions)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(policyName,
                policy => policy.Requirements.Add(new PermissionRequirement(permissions)));
        });
    }
    
}