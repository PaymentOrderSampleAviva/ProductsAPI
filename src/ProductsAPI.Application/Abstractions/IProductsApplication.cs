using ProductsAPI.Application.DTOs;

namespace ProductsAPI.Application.Abstractions
{
    public interface IProductsApplication
    {
        Task<IReadOnlyList<ProductDto>> GetProductsAsync(CancellationToken cancellationToken = default);
    }
}
