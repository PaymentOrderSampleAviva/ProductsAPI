using Microsoft.EntityFrameworkCore;
using ProductsAPI.Models;
using ProductsAPI.Repositories;

namespace ProductsAPI.Data;

public class ProductsRepository(ApplicationDbContext dbContext) : IProductsRepository
{
	public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default)
	{
		return await dbContext.Products.AsNoTracking().ToListAsync(cancellationToken);
	}

	public async Task<int> GetAvailableCountAsync(List<int> productIds, CancellationToken cancellationToken = default)
	{
		return await dbContext.Products
			.Where(p => p.Status == ProductStatus.Available)
			.Where(p => productIds.Contains(p.Id))
			.CountAsync(cancellationToken);
	}
}
