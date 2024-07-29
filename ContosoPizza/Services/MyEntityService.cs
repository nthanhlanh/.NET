using ContosoPizza.Models;
using ContosoPizza.Repositories;

namespace ContosoPizza.Services;

public class MyEntityService : IMyEntityService
{
    private readonly IMyEntityRepository _myEntityRepository;

    public MyEntityService(IMyEntityRepository myEntityRepository)
    {
        _myEntityRepository = myEntityRepository;
    }

    public async Task<IEnumerable<MyEntity>> GetAllAsync()
    {
        return await _myEntityRepository.GetAllAsync();
    }

    public async Task<MyEntity> GetByIdAsync(int id)
    {
        return await _myEntityRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(MyEntity entity)
    {
        await _myEntityRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(MyEntity entity)
    {
        await _myEntityRepository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _myEntityRepository.DeleteAsync(id);
    }
}