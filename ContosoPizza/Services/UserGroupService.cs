using ContosoPizza.Models;
using ContosoPizza.Repositories;

namespace ContosoPizza.Services
{
    public class UserGroupService : IUserGroupService
    {
        private readonly IUserGroupRepository _userGroupRepository;

        public UserGroupService(IUserGroupRepository userGroupRepository)
        {
            _userGroupRepository = userGroupRepository;
        }

        public async Task<IEnumerable<Group>> GetUserGroupsAsync(string userId)
        {
            return await _userGroupRepository.GetUserGroupsAsync(userId);
        }

        public async Task AddUserToGroupAsync(string userId, int groupId)
        {
            await _userGroupRepository.AddUserToGroupAsync(userId, groupId);
        }

        public async Task RemoveUserFromGroupAsync(string userId, int groupId)
        {
            await _userGroupRepository.RemoveUserFromGroupAsync(userId, groupId);
        }
    }
}
