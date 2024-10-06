using ProductsAPI.DTOs;

namespace ProductsAPI.AppServices.Abstractions
{
    public interface IOrderAppService
    {
        Task<object> CreateOrderAsync(CreateOrderDto request);
    }
}
