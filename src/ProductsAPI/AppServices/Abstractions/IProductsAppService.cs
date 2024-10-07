using ProductsAPI.DTOs;

namespace ProductsAPI.AppServices.Abstractions
{
    public interface IProductsAppService
    {
        Task<IReadOnlyList<ProductDto>> ListProductsAsync(CancellationToken cancellationToken = default);
    }
}
