// AuthService.cs
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ContosoPizza.Data;
using ContosoPizza.Dto;
using ContosoPizza.Helprs;
using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ContosoPizza.Controllers;
public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _config;
    private readonly ILogger<AuthService> _logger;

    public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context, IConfiguration config, ILogger<AuthService> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _context = context;
        _config = config;
        _logger = logger;
    }

    public async Task<string> CreateTokenAsync(ApplicationUser user, int expires)
    {
        var userName = user.UserName;
        if (userName == null)
        {
            throw new ArgumentNullException("The userName is null ", nameof(userName));
        }
        var roles = await _userManager.GetRolesAsync(user);
        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        foreach (var role in roles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config["Jwt:SecretKey"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(authClaims),
            Expires = DateTime.UtcNow.AddDays(expires),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"]
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<RefreshToken> CreateRefreshTokenAsync(ApplicationUser user)
    {
        return new RefreshToken
        {
            Token = await CreateTokenAsync(user, 7),
            Expires = DateTime.UtcNow.AddDays(7),
            Created = DateTime.UtcNow,
            UserId = user.Id
        };
    }

    public async Task<LoginResponse> LoginAsync(Login login)
    {
        var user = await _userManager.FindByEmailAsync(login.Username);
        if (user == null)
            throw new UnauthorizedAccessException("Invalid email or password");

        var result = await _signInManager.PasswordSignInAsync(login.Username, login.Password, login.RememberMe, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            var tokenString = await CreateTokenAsync(user, 1);
            var refreshToken = await CreateRefreshTokenAsync(user);
            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            return new LoginResponse { Token = tokenString, RefreshToken = refreshToken };
        }

        throw new UnauthorizedAccessException("Invalid login attempt");
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<IActionResult> RefreshTokenAsync(RefreshModel model)
    {
        var refreshToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == model.Token);

        if (refreshToken == null || !refreshToken.IsActive)
        {
            return new UnauthorizedResult();
        }
        var user = await _userManager.FindByIdAsync(refreshToken.UserId);
        if (user == null)
        {
            _logger.LogError("An error occurred while getting Refresh. The User is null.");
            return new NotFoundObjectResult("The User is null.");
        }
        var newToken = await CreateTokenAsync(user, 1);
        var refreshTokenNew = await CreateRefreshTokenAsync(user);

        refreshToken.IsRevoked = true;
        _context.RefreshTokens.Add(refreshTokenNew);
        await _context.SaveChangesAsync();

        return new OkObjectResult(new { token = newToken, refreshToken = refreshTokenNew });
    }

    public async Task<IActionResult> RegisterAsync(Login model)
    {
        var user = new ApplicationUser { UserName = model.Username, Email = model.Username };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            return new BadRequestObjectResult(result.Errors);

        return new OkObjectResult("User created successfully");
    }

    public async Task<IActionResult> AssignRoleToUserAsync(string email, string roleName)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return new BadRequestObjectResult("User not found");

        var roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
            return new BadRequestObjectResult("Role not found");

        var result = await _userManager.AddToRoleAsync(user, roleName);
        if (result.Succeeded)
        {
            return new OkObjectResult($"Role {roleName} assigned to user {email} successfully");
        }

        return new BadRequestObjectResult(result.Errors);
    }
}
