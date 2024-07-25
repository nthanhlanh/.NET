using ContosoPizza.Data;
using ContosoPizza.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Services;

public class PizzaService :IPizzaService
{
    private readonly ApplicationDbContext _context;
    public PizzaService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Pizza>> GetAllAsync(){
        return await _context.MyPizzas.ToArrayAsync();
    }

    public async Task<Pizza> GetByIdAsync(int id)
    {
#pragma warning disable CS8603 // Possible null reference return.
        return await _context.MyPizzas.FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
    }

    public async Task AddAsync(Pizza pizza)
    {
        _context.MyPizzas.Add(pizza);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(Pizza pizza)
    {
        _context.MyPizzas.Update(pizza);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var entity = await _context.MyPizzas.FindAsync(id);
        if (entity != null)
        {
        _context.MyPizzas.Remove(entity);
        await _context.SaveChangesAsync();
        }
    }
}    
