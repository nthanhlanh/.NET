using ContosoPizza.Data;
using ContosoPizza.Dto;
using ContosoPizza.Helprs;
using ContosoPizza.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace ContosoPizza.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;


        public AuthController(ApplicationDbContext context,
        IConfiguration config, UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager,
        IConfiguration configuration)
        {
            _context = context;
            _config = config;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Login model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new ApplicationUser { UserName = model.Username, Email = model.Username };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // Thêm claim tùy chỉnh
            //await _userManager.AddClaimAsync(user, new Claim("CustomClaimType", "CustomClaimValue"));

            return Ok("User created successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            var user = await _userManager.FindByEmailAsync(login.Username);
            if (user == null)
                return Unauthorized(new { message = "Invalid email or password" });

            var result = await _signInManager.PasswordSignInAsync(login.Username, login.Password, login.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded) // Replace with your user validation logic
            {

                var tokenString = createToken(user, 1);
                var refreshToken = await CreateRefreshToken(user);
                _context.RefreshTokens.Add(refreshToken);
                await _context.SaveChangesAsync();

                return Ok(new { Token = tokenString, RefreshToken = refreshToken });
            }
            return Unauthorized(new { Message = "Invalid login attempt" });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { message = "Logout successful" });
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRoleToUser(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return BadRequest("User not found");

            var roleExists = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExists)
                return BadRequest("Role not found");

            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (result.Succeeded)
            {
                return Ok($"Role {roleName} assigned to user {email} successfully");
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshModel model)
        {
            var refreshToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == model.Token);

            // string jsonString = JsonSerializer.Serialize(refreshToken, new JsonSerializerOptions { WriteIndented = true });
            // Console.WriteLine(jsonString);

            if (refreshToken == null || !refreshToken.IsActive)
            {
                return Unauthorized();
            }
            var user = await _userManager.FindByIdAsync(refreshToken.UserId);
            if (user == null )
            {
                 return  NotFound("The User is null.");
            }
            var newToken = await createToken(user, 1);
            var refreshTokenNew = await CreateRefreshToken(user);

            refreshToken.IsRevoked = true;
            _context.RefreshTokens.Add(refreshTokenNew);
            await _context.SaveChangesAsync();

            return Ok(new { token = newToken, refreshToken = refreshTokenNew });
        }

        private async Task<String> createToken(ApplicationUser user, int expires)
        {
            var roles = await _userManager.GetRolesAsync(user);
#pragma warning disable CS8604 // Possible null reference argument.
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
#pragma warning restore CS8604 // Possible null reference argument.

            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
#pragma warning disable CS8604 // Possible null reference argument.
            var key = Encoding.ASCII.GetBytes(_config["Jwt:SecretKey"]); // Replace with your secret key
#pragma warning restore CS8604 // Possible null reference argument.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(authClaims),
                Expires = DateTime.UtcNow.AddDays(expires),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"]
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

        private async Task<RefreshToken> CreateRefreshToken(ApplicationUser user)
        {
            return new RefreshToken
            {
                Token = await createToken(user, 7),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                UserId = user.Id
            };
        }

    }
}





