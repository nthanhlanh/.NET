
using ContosoPizza.Helprs;

namespace ContosoPizza.Services
{
    public interface IApplicationUserService
    {
        Task<(IEnumerable<ApplicationUser> users, int totalCount)> GetUsersAsync(int page, int pageSize);
        Task<ApplicationUser> GetUserFromTokenAsync(string token);
        bool IsTokenExpired(string token);
    }
}
