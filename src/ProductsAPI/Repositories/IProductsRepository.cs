using ProductsAPI.Models;

namespace ProductsAPI.Repositories
{
	public interface IProductsRepository
	{
		Task<IReadOnlyList<Product>> GetAllAsync();
	}
}
