using System.Text.Json;
using ApiGateway.Services.RabbitMq;

namespace ApiGateway.Middlewares;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceProvider _serviceProvider;

    public AuthMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
    {
        _next = next;
        _serviceProvider = serviceProvider;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var producer = scope.ServiceProvider.GetRequiredService<IMessageProducer>();
            var token = context.Request.Cookies["suchatastycookie"];
            if (!string.IsNullOrEmpty(token))
            {
                var json = await producer.SendMessageAsync(token);

                var data = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
                var userId = data["UserId"].ToString();
                var permissions = (JsonElement)data["Permissions"];
                var userName = data["UserName"].ToString();

                var permissionsList = permissions.EnumerateArray()
                    .Select(p => p.GetString())
                    .ToList();

                context.Request.Headers["X-User-Id"] = userId;
                context.Request.Headers["X-User-Name"] = userName;
                context.Request.Headers["X-User-Permissions"] = string.Join(",", permissionsList);
            }
        }

        await _next(context);
    }
}