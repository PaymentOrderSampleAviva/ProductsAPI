using Microsoft.EntityFrameworkCore;
using ProductsAPI.Models;
using ProductsAPI.Repositories;
using Throw;

namespace ProductsAPI.Data
{
	public class OrdersRepository(ApplicationDbContext dbContext) : IOrdersRepository
	{
		private readonly ApplicationDbContext _dbContext = dbContext;

		public async Task<Order> AddAsync(Order order, CancellationToken cancellationToken = default)
		{
			order.ThrowIfNull();
			_dbContext.Orders.Add(order);
			await _dbContext.SaveChangesAsync(cancellationToken);
			return order;
		}

		public async Task<IReadOnlyList<Order>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			return await _dbContext.Orders
				.Include(e => e.Fees)
				.Include(e => e.Items)
				.ThenInclude(e => e.Product)
				.AsNoTracking()
				.ToListAsync(cancellationToken);
		}

		public async Task<Order?> GetAsync(int id, bool enableTraking = false, CancellationToken cancellationToken = default)
		{
			var query = _dbContext.Orders
				.Where(o => o.Id == id)
				.Include(e => e.Fees)
				.Include(e => e.Items)
				.ThenInclude(e => e.Product);

			return enableTraking ? await query.FirstOrDefaultAsync(cancellationToken) : await query.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
		}

		public async Task<Order> UpdateAsync(Order order, CancellationToken cancellationToken = default)
		{
			_dbContext.Orders.Update(order);
			await _dbContext.SaveChangesAsync(cancellationToken);
			return order;
		}
	}
}
