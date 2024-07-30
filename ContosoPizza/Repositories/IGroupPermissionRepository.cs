using ContosoPizza.Models;
namespace ContosoPizza.Repositories
{

    public interface IGroupPermissionRepository
    {
        Task<IEnumerable<Permission>> GetPermissionsByGroupIdAsync(int groupId);
        Task AddPermissionToGroupAsync(int groupId, int permissionId);
        Task RemovePermissionFromGroupAsync(int groupId, int permissionId);
    }
}
