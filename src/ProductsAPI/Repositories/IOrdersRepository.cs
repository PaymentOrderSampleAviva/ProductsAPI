using ProductsAPI.Models;

namespace ProductsAPI.Repositories
{
	public interface IOrdersRepository
	{
		Task<IReadOnlyList<Order>> GetAllAsync();
		Task<Order?> GetAsync(int id, bool enableTraking = false);
		Task<Order> AddAsync(Order order);
		Task<Order> UpdateAsync(Order order);
	}
}
