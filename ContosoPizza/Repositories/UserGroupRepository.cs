using ContosoPizza.Data;
using ContosoPizza.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Repositories
{
    public class UserGroupRepository : IUserGroupRepository
    {
        private readonly ApplicationDbContext _context;

        public UserGroupRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Group>> GetUserGroupsAsync(string userId)
        {
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return await _context.UserGroups
                .Where(ug => ug.UserId == userId)
                .Select(ug => ug.Group)
                .ToListAsync();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        public async Task AddUserToGroupAsync(string userId, int groupId)
        {
            var userGroup = new UserGroup { UserId = userId, GroupId = groupId };
            _context.UserGroups.Add(userGroup);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUserFromGroupAsync(string userId, int groupId)
        {
            var userGroup = await _context.UserGroups
                .FirstOrDefaultAsync(ug => ug.UserId == userId && ug.GroupId == groupId);
            if (userGroup != null)
            {
                _context.UserGroups.Remove(userGroup);
                await _context.SaveChangesAsync();
            }
        }
    }
}
