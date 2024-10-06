using ProductsAPI.DTOs;

namespace ProductsAPI.AppServices.Abstractions
{
    public interface IOrdersAppService
    {
		Task<IReadOnlyList<OrderDto>> ListOrdersAsync();

		Task<OrderDto> CreateOrderAsync(CreateOrderDto request);
        Task<OrderDto> CancelOrderAsync(int orderId, string reason);

	}
}
