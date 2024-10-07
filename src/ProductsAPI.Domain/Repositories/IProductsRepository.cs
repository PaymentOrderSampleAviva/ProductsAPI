using ProductsAPI.Domain.Entities;

namespace ProductsAPI.Domain.Repositories
{
	public interface IProductsRepository
	{
		Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default);
		Task<int> GetAvailableCountAsync(List<int> productIds, CancellationToken cancellationToken = default);
	}
}
