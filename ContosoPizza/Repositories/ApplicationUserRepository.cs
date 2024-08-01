using ContosoPizza.Data;
using ContosoPizza.Helprs;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
