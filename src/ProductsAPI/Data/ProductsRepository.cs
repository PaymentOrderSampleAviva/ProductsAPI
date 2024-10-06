using Microsoft.EntityFrameworkCore;
using ProductsAPI.Models;
using ProductsAPI.Repositories;

namespace ProductsAPI.Data;

public class ProductsRepository(ApplicationDbContext dbContext) : IProductsRepository
{
	private readonly ApplicationDbContext _dbContext = dbContext;

	public async Task<IReadOnlyList<Product>> GetAllAsync()
	{
		return await _dbContext.Products.AsNoTracking().ToListAsync();
	}
}
