using ProductsAPI.Application.DTOs;

namespace ProductsAPI.Application.Abstractions
{
    public interface IOrdersApplication
    {
        Task<IReadOnlyList<OrderDto>> GetOrdersAsync(CancellationToken cancellationToken = default);

        Task<OrderDto> CreateOrderAsync(CreateOrderDto request, CancellationToken cancellationToken = default);
        Task<OrderDto> CancelOrderAsync(int orderId, string reason, CancellationToken cancellationToken = default);
    }
}
