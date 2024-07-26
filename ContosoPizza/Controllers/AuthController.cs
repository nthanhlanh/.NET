using ContosoPizza.Helprs;
using ContosoPizza.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ContosoPizza.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;


        public AuthController(
        IConfiguration config, UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager,
        IConfiguration configuration)
        {
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
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    Issuer = _config["Jwt:Issuer"],
                    Audience = _config["Jwt:Audience"]
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new { Token = tokenString });
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

    }
}





