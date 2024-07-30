using ContosoPizza.Models;

namespace ContosoPizza.Services
{
    public interface IUserGroupService
    {
        Task<IEnumerable<Group>> GetUserGroupsAsync(string userId);
        Task AddUserToGroupAsync(string userId, int groupId);
        Task RemoveUserFromGroupAsync(string userId, int groupId);
    }
}
