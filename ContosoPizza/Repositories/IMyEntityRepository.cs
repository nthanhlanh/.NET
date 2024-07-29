using ContosoPizza.Models;

namespace ContosoPizza.Repositories
{
    public interface IMyEntityRepository
    {
        Task<MyEntity> GetByIdAsync(int id);
        Task<IEnumerable<MyEntity>> GetAllAsync();
        Task AddAsync(MyEntity entity);
        Task UpdateAsync(MyEntity entity);
        Task DeleteAsync(int id);
    }
}
