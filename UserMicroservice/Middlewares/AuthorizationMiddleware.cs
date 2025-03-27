
using System.Text;
using Newtonsoft.Json;
using UserMicroservice.Enums;
using UserMicroservice.Infrastructure;

namespace UserMicroservice.Middlewares
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthorizationMiddleware(RequestDelegate next, IHttpClientFactory httpClientFactory)
        {
            _next = next;
            _httpClientFactory = httpClientFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Cookies["suchatastycookie"];
            
            var permissionAttributes = context
                .GetEndpoint()?.Metadata.OfType<PermissionAttribute>().ToList();
            
            var permissionsList = new List<Permission>();

            if (permissionAttributes != null && permissionAttributes.Count != 0)
            {
                foreach (var permissionAttribute in permissionAttributes)
                {
                    var policy = permissionAttribute.Policy;

                    if (!string.IsNullOrEmpty(policy))
                    {
                        if (Enum.TryParse<Permission>(policy, true, out var permission))
                        {
                            permissionsList.Add(permission);
                        }
                        else
                        {
                            Console.WriteLine($"Неизвестная политика: {policy}");
                        }
                    }
                }
            }

            var arr = permissionsList.ToArray();
            
            var requirement = new PermissionRequirement(arr);

            var client = _httpClientFactory.CreateClient();
            
            var payload = new
            {
                token = token,          
                requirement = new       
                {
                    Permissions = requirement.Permissions 
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:7002/api/Users/check-requirenments", content);
            
            if (response.IsSuccessStatusCode)
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("You don't have permissions for this resource");
            }
        }
    }
}