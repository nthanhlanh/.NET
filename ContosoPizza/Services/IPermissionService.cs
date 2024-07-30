using ContosoPizza.Models;

namespace ContosoPizza.Services
{
    public interface IPermissionService
    {
        Task<IEnumerable<Permission>> GetAllPermissionsAsync();
        Task<Permission> GetPermissionByIdAsync(int id);
        Task<Permission> CreatePermissionAsync(Permission permission);
        Task<Permission> UpdatePermissionAsync(Permission permission);
        Task DeletePermissionAsync(int id);
        Task<IEnumerable<PermissionTreeNode>> GetPermissionTreeByParentIdAsync(int parentId);
    }
}
