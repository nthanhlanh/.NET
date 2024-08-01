using ContosoPizza.Helprs;

namespace ContosoPizza.Repositories
{
    public interface IApplicationUserRepository
    {
        Task<ApplicationUser> GetUserByIdAsync(string userId);
    }
}
