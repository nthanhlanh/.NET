using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ContosoPizza.Data;
using ContosoPizza.Models;
using ContosoPizza.Repositories;
namespace ContosoPizza.Repositories
{
    public class GroupPermissionRepository : IGroupPermissionRepository
    {
        private readonly ApplicationDbContext _context;

        public GroupPermissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Permission>> GetPermissionsByGroupIdAsync(int groupId)
        {
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return await _context.GroupPermissions
                .Where(gp => gp.GroupId == groupId)
                .Select(gp => gp.Permission)
                .ToListAsync();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        public async Task AddPermissionToGroupAsync(int groupId, int permissionId)
        {
            var groupPermission = new GroupPermission
            {
                GroupId = groupId,
                PermissionId = permissionId
            };

            _context.GroupPermissions.Add(groupPermission);
            await _context.SaveChangesAsync();
        }

        public async Task RemovePermissionFromGroupAsync(int groupId, int permissionId)
        {
            var groupPermission = await _context.GroupPermissions
                .Where(gp => gp.GroupId == groupId && gp.PermissionId == permissionId)
                .SingleOrDefaultAsync();

            if (groupPermission != null)
            {
                _context.GroupPermissions.Remove(groupPermission);
                await _context.SaveChangesAsync();
            }
        }
    }
}
