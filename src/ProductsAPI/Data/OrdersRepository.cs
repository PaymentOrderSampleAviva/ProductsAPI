using Microsoft.EntityFrameworkCore;
using ProductsAPI.Models;
using ProductsAPI.Repositories;
using Throw;

namespace ProductsAPI.Data
{
	public class OrdersRepository(ApplicationDbContext dbContext) : IOrdersRepository
	{
		private readonly ApplicationDbContext _dbContext = dbContext;

		public async Task<Order> AddAsync(Order order)
		{
			order.ThrowIfNull();
			_dbContext.Orders.Add(order);
			await _dbContext.SaveChangesAsync();
			return order;
		}

		public async Task<IReadOnlyList<Order>> GetAllAsync()
		{
			return await _dbContext.Orders
				.Include(e => e.Fees)
				.Include(e => e.Items)
				.ThenInclude(e => e.Product)
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<Order?> GetAsync(int id, bool enableTraking = false)
		{
			var query = _dbContext.Orders
				.Where(o => o.Id == id)
				.Include(e => e.Fees)
				.Include(e => e.Items)
				.ThenInclude(e => e.Product);

			return enableTraking ? await query.FirstOrDefaultAsync() : await query.AsNoTracking().FirstOrDefaultAsync();
		}

		public async Task<Order> UpdateAsync(Order order)
		{
			_dbContext.Orders.Update(order);
			await _dbContext.SaveChangesAsync();
			return order;
		}
	}
}
