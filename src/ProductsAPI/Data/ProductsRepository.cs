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
}
