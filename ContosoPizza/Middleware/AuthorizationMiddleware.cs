using System.Security.Claims;
using System.Text.Json;
using ContosoPizza.Repositories;

namespace ContosoPizza.Helprs
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUserGroupRepository iUserGroupRepository, IGroupPermissionRepository iGroupPermissionRepository)
        {
            if (context.Request.Path == "/api/auth/login")
            {
                // Bỏ qua xác thực
                await _next(context);
                return;
            }

            if (context.User == null || context.User.Identity == null)
            {
                throw new ArgumentNullException("The context.User is null  ", nameof(context.User));
            }
            if (!context.User.Identity.IsAuthenticated)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("User is not authenticated.");
                return;
            }
            // Lấy thông tin người dùng từ HttpContext
            var userId = context.User?.Identity?.Name;
            if (userId != null)
            {
                // Lấy danh sách các nhóm mà người dùng thuộc về
                var groups = await iUserGroupRepository.GetUserGroupsAsync(userId);

                // Kiểm tra quyền của từng nhóm
                foreach (var group in groups)
                {
                    var permissions = await iGroupPermissionRepository.GetPermissionsByGroupIdAsync(group.Id);
                    // Kiểm tra xem nhóm có quyền cần thiết không
                    if (permissions.Any(p => p.Name == context.Request.Path))
                    {
                        // Nếu có quyền, tiếp tục xử lý
                        await _next(context);
                        return;
                    }
                }
            }
            // Nếu không có quyền, trả về lỗi 403 Forbidden
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Forbidden: You do not have the required permissions.");

        }
    }
}
