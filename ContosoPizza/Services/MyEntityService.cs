using ContosoPizza.Data;
using ContosoPizza.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Services;

public class MyEntityService : IMyEntityService
{
    private readonly ApplicationDbContext _context;

    public MyEntityService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MyEntity>> GetAllAsync()
    {
        return await _context.MyEntities.ToListAsync();
    }

    public async Task<MyEntity> GetByIdAsync(int id)
    {
#pragma warning disable CS8603 // Possible null reference return.
        return await _context.MyEntities.FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
    }

    public async Task AddAsync(MyEntity entity)
    {
        _context.MyEntities.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(MyEntity entity)
    {
        _context.MyEntities.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.MyEntities.FindAsync(id);
        if (entity != null)
        {
            _context.MyEntities.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}