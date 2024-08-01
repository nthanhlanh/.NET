using System.IdentityModel.Tokens.Jwt;
using ContosoPizza.Helprs;
using ContosoPizza.Repositories;
using Microsoft.AspNetCore.Identity;

namespace ContosoPizza.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUserRepository _userRepository;

        public ApplicationUserService(UserManager<ApplicationUser> userManager,IApplicationUserRepository userRepository)
        {
             _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<(IEnumerable<ApplicationUser> users, int totalCount)> GetUsersAsync(int page, int pageSize)
        {
            var totalCount = _userManager.Users.Count();
            var users = _userManager.Users
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return await Task.FromResult((users, totalCount));
        }

        public async Task<ApplicationUser> GetUserFromTokenAsync(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
                throw new ArgumentNullException("The jwtToken is null ", nameof(jwtToken));
            var userId = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "unique_name");
            if (userId == null)
                throw new ArgumentNullException("The userId is null ", nameof(userId));
            return await _userRepository.GetUserByIdAsync(userId.Value);
        }

        public bool IsTokenExpired(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
                return true;

            var exp = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "exp")?.Value;

            if (exp == null)
                return true;

            var expDate = DateTimeOffset.FromUnixTimeSeconds(long.Parse(exp)).UtcDateTime;

            return expDate < DateTime.UtcNow;
        }
    }
}
