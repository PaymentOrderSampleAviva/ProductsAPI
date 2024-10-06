using ProductsAPI.DataContracts;

namespace ProductsAPI.AppServices
{
	public interface IOrderAppService
	{
		Task<object> CreateOrderAsync(CreateOrderRequest request);
	}
}
