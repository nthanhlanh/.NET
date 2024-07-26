using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ContosoPizza.Helprs
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //var pathsToSkip = new[] { "/auth/login" };//, "/api/auth/register"

            if (context.Request.Path == "/auth/login" || context.Request.Path == "/auth/register")
            {
                // Bỏ qua xác thực
                await _next(context);
            }
            else
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                if (!context.User.Identity.IsAuthenticated)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("User is not authenticated.");
                    return;
                }
                // Kiểm tra quyền truy cập
                var userRoles = context.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
                foreach (var user in userRoles)
                {
                    Console.WriteLine($"Type: {user}");
                }
                // if (!userRoles.Contains("Admin"))
                // {
                //     context.Response.StatusCode = StatusCodes.Status403Forbidden;
                //     await context.Response.WriteAsync("User does not have the necessary role.");
                //     return;
                // }
                await _next(context);
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        }
    }
}
