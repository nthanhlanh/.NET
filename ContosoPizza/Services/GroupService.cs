using ContosoPizza.Models;
using ContosoPizza.Repositories;

namespace ContosoPizza.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<IEnumerable<Group>> GetAllGroupsAsync()
        {
            return await _groupRepository.GetAllGroupsAsync();
        }

        public async Task<Group> GetGroupByIdAsync(int id)
        {
            return await _groupRepository.GetGroupByIdAsync(id);
        }

        public async Task<Group> CreateGroupAsync(Group group)
        {
            return await _groupRepository.CreateGroupAsync(group);
        }

        public async Task<Group> UpdateGroupAsync(Group group)
        {
            return await _groupRepository.UpdateGroupAsync(group);
        }

        public async Task DeleteGroupAsync(int id)
        {
            await _groupRepository.DeleteGroupAsync(id);
        }
    }
}
