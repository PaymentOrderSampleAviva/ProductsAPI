using ProductsAPI.DataContracts;

namespace ProductsAPI.AppServices.Abstractions
{
    public interface IOrderAppService
    {
        Task<object> CreateOrderAsync(CreateOrderRequest request);
    }
}
