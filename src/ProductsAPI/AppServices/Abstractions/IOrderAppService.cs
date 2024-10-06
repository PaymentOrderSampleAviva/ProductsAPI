using ProductsAPI.DTOs;

namespace ProductsAPI.AppServices.Abstractions
{
    public interface IOrderAppService
    {
		Task<IReadOnlyList<OrderDto>> ListOrdersAsync();

		Task<OrderViewDto> CreateOrderAsync(CreateOrderDto request);
        Task<OrderViewDto> CancelOrderAsync(int orderId, string reason);

	}
}
