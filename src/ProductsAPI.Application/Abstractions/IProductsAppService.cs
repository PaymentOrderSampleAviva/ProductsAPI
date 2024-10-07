using ProductsAPI.Application.DTOs;

namespace ProductsAPI.Application.Abstractions
{
    public interface IProductsAppService
    {
        Task<IReadOnlyList<ProductDto>> GetProductsAsync(CancellationToken cancellationToken = default);
    }
}
