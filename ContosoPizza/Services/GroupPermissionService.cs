using ContosoPizza.Models;
using ContosoPizza.Repositories;
namespace ContosoPizza.Services
{

    public class GroupPermissionService : IGroupPermissionService
    {
        private readonly IGroupPermissionRepository _groupPermissionRepository;

        public GroupPermissionService(IGroupPermissionRepository groupPermissionRepository)
        {
            _groupPermissionRepository = groupPermissionRepository;
        }

        public async Task<IEnumerable<Permission>> GetPermissionsByGroupIdAsync(int groupId)
        {
            return await _groupPermissionRepository.GetPermissionsByGroupIdAsync(groupId);
        }

        public async Task AddPermissionToGroupAsync(int groupId, int permissionId)
        {
            await _groupPermissionRepository.AddPermissionToGroupAsync(groupId, permissionId);
        }

        public async Task RemovePermissionFromGroupAsync(int groupId, int permissionId)
        {
            await _groupPermissionRepository.RemovePermissionFromGroupAsync(groupId, permissionId);
        }
    }
}