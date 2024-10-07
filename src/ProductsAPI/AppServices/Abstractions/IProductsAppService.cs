using ProductsAPI.DTOs;

namespace ProductsAPI.AppServices.Abstractions
{
    public interface IProductsAppService
    {
        Task<IReadOnlyList<ProductDto>> ListAllAsync(CancellationToken cancellationToken = default);
    }
}
