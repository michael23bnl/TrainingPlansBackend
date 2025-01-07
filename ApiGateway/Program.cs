    using System.Text;
    using Ocelot.DependencyInjection;
    using Ocelot.Middleware;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.IdentityModel.Tokens;
    

    var builder = WebApplication.CreateBuilder(args);

    builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
    builder.Services.AddOcelot();
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials(); 
        });
    });
    
    
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

    app.UseCors(); 
    

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    
    app.UseOcelot().Wait();

    app.Run();
    