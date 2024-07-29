using ContosoPizza.Dto;
using ContosoPizza.Helprs;
using ContosoPizza.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Services;
public interface IAuthService
{
    Task<string> CreateTokenAsync(ApplicationUser user, int expires);
    Task<RefreshToken> CreateRefreshTokenAsync(ApplicationUser user);
    Task<LoginResponse> LoginAsync(Login login);
    Task LogoutAsync();
    Task<IActionResult> RefreshTokenAsync(RefreshModel model);
    Task<IActionResult> RegisterAsync(Login model);
    Task<IActionResult> AssignRoleToUserAsync(string email, string roleName);
}