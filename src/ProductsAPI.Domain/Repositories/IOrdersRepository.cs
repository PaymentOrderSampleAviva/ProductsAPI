using ProductsAPI.Domain.Entities;

namespace ProductsAPI.Domain.Repositories
{
	public interface IOrdersRepository
	{
		Task<IReadOnlyList<Order>> GetAllAsync(CancellationToken cancellationToken = default);
		Task<Order?> GetAsync(int id, bool enableTraking = false, CancellationToken cancellationToken = default);
		Task<Order> AddAsync(Order order, CancellationToken cancellationToken = default);
		Task<Order> UpdateAsync(Order order, CancellationToken cancellationToken = default);
	}
}
