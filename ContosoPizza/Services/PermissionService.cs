using ContosoPizza.Models;
using ContosoPizza.Repositories;

namespace ContosoPizza.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<IEnumerable<Permission>> GetAllPermissionsAsync()
        {
            return await _permissionRepository.GetAllPermissionsAsync();
        }

        public async Task<Permission> GetPermissionByIdAsync(int id)
        {
            return await _permissionRepository.GetPermissionByIdAsync(id);
        }

        public async Task<Permission> CreatePermissionAsync(Permission permission)
        {
            return await _permissionRepository.CreatePermissionAsync(permission);
        }

        public async Task<Permission> UpdatePermissionAsync(Permission permission)
        {
            return await _permissionRepository.UpdatePermissionAsync(permission);
        }

        public async Task DeletePermissionAsync(int id)
        {
            await _permissionRepository.DeletePermissionAsync(id);
        }

        public async Task<IEnumerable<PermissionTreeNode>> GetPermissionTreeByParentIdAsync(int parentId)
        {
            var permissions = await _permissionRepository.GetAllPermissionsAsync();
            var nodes = permissions
                .Where(p => p.ParentId == parentId)
                .Select(p => new PermissionTreeNode
                {
                    Id = p.Id,
                    Name = p.Name,
                    Children = GetChildren(p.Id, permissions).ToList()
                });
            return nodes;
        }

        private IEnumerable<PermissionTreeNode> GetChildren(int parentId, IEnumerable<Permission> allPermissions)
        {
            return allPermissions
                .Where(p => p.ParentId == parentId)
                .Select(p => new PermissionTreeNode
                {
                    Id = p.Id,
                    Name = p.Name,
                    Children = GetChildren(p.Id, allPermissions).ToList()
                });
        }
    }
}
