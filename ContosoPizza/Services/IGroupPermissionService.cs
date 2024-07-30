using ContosoPizza.Models;
namespace ContosoPizza.Services
{

    public interface IGroupPermissionService
    {
        Task<IEnumerable<Permission>> GetPermissionsByGroupIdAsync(int groupId);
        Task AddPermissionToGroupAsync(int groupId, int permissionId);
        Task RemovePermissionFromGroupAsync(int groupId, int permissionId);
    }
}