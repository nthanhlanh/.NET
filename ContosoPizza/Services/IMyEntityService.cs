using ContosoPizza.Data;
using ContosoPizza.Models;

public interface IMyEntityService
{
    Task<IEnumerable<MyEntity>> GetAllAsync();
    Task<MyEntity> GetByIdAsync(int id);
    Task AddAsync(MyEntity entity);
    Task UpdateAsync(MyEntity entity);
    Task DeleteAsync(int id);
}