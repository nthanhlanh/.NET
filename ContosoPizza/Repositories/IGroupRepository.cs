using ContosoPizza.Models;

namespace ContosoPizza.Repositories
{
    public interface IGroupRepository
    {
        Task<IEnumerable<Group>> GetAllGroupsAsync();
        Task<Group> GetGroupByIdAsync(int id);
        Task<Group> CreateGroupAsync(Group group);
        Task<Group> UpdateGroupAsync(Group group);
        Task DeleteGroupAsync(int id);
    }
}
