using ContosoPizza.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContosoPizza.Repositories
{
    public interface IUserGroupRepository
    {
        Task<IEnumerable<Group>> GetUserGroupsAsync(string userId);
        Task AddUserToGroupAsync(string userId, int groupId);
        Task RemoveUserFromGroupAsync(string userId, int groupId);
    }
}
