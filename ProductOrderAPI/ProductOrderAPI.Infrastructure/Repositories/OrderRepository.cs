using Microsoft.EntityFrameworkCore;
using ProductOrderAPI.Domain.Entities;
using ProductOrderAPI.Domain.Interfaces;
using ProductOrderAPI.Infrastructure.Data;

namespace ProductOrderAPI.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly StoreDbContext _context;

    public OrderRepository(StoreDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _context.Orders
            .Include(o => o.Product)
            .ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        return await _context.Orders
            .Include(o => o.Product)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task AddAsync(Order entity)
    {
        await _context.Orders.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order entity)
    {
        _context.Orders.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Order entity)
    {
        _context.Orders.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<(IEnumerable<Order> Orders, int TotalCount)> GetPagedAsync(
        int pageNumber, int pageSize,
        DateTime? dateFrom, DateTime? dateTo)
    {
        var query = _context.Orders
            .Include(o => o.Product)
            .AsQueryable();

        if (dateFrom.HasValue)
            query = query.Where(o => o.OrderDate >= dateFrom.Value);
        if (dateTo.HasValue)
            query = query.Where(o => o.OrderDate <= dateTo.Value);

        var totalCount = await query.CountAsync();

        var orders = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (orders, totalCount);
    }
}