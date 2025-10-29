using Microsoft.EntityFrameworkCore;
using ProvaDev.Domain.Entities.Orders;
using ProvaDev.Domain.Repositories;
using ProvaDev.Infrastructure.Data;
using ProvaDev.Infrastructure.Data.Context;

namespace ProvaDev.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Items)
                .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Items)
                .ThenInclude(oi => oi.Product)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Order>> GetOpenOrdersAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Items)
                .ThenInclude(oi => oi.Product)
            .Where(o => !o.IsClosed)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Order order, CancellationToken cancellationToken = default)
    {
        await _context.Orders.AddAsync(order, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Order order, CancellationToken cancellationToken = default)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var order = await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
            
        if (order != null)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}