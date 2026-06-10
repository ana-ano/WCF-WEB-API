using Microsoft.EntityFrameworkCore;
using ProductOrderAPI.Domain.Entities;
using ProductOrderAPI.Domain.Interfaces;
using ProductOrderAPI.Infrastructure.Data;

namespace ProductOrderAPI.Infrastructure.Repositories;

public class CategoryRepository : IRepository<Category>
{
    private readonly StoreDbContext _context;

    public CategoryRepository(StoreDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public async Task AddAsync(Category entity)
    {
        await _context.Categories.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category entity)
    {
        _context.Categories.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Category entity)
    {
        _context.Categories.Remove(entity);
        await _context.SaveChangesAsync();
    }
}