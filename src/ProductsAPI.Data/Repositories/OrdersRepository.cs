using Microsoft.EntityFrameworkCore;
using ProductsAPI.Domain.Entities;
using ProductsAPI.Domain.Repositories;
using Throw;

namespace ProductsAPI.Data.Repositories
{
    public class OrdersRepository(ApplicationDbContext dbContext) : IOrdersRepository
    {
        public async Task<Order> AddAsync(Order order, CancellationToken cancellationToken = default)
        {
            order.ThrowIfNull();
            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync(cancellationToken);
            return order;
        }

        public async Task<IReadOnlyList<Order>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.Orders
                .Include(e => e.Fees)
                .Include(e => e.Items)
                .ThenInclude(e => e.Product)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Order?> GetAsync(int id, bool enableTraking = false, CancellationToken cancellationToken = default)
        {
            var query = dbContext.Orders
                .Where(o => o.Id == id)
                .Include(e => e.Fees)
                .Include(e => e.Items)
                .ThenInclude(e => e.Product);

            return enableTraking ? await query.FirstOrDefaultAsync(cancellationToken) : await query.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Order> UpdateAsync(Order order, CancellationToken cancellationToken = default)
        {
            dbContext.Orders.Update(order);
            await dbContext.SaveChangesAsync(cancellationToken);
            return order;
        }
    }
}
