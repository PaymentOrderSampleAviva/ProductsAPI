using AutoMapper;
using ProductsAPI.Application.DTOs;
using ProductsAPI.Domain.Repositories;
using ProductsAPI.Application.Abstractions;

namespace ProductsAPI.Application;

public class ProductsAppService(IProductsRepository repository, IMapper mapper) : IProductsAppService
{
    public async Task<IReadOnlyList<ProductDto>> GetProductsAsync(CancellationToken cancellationToken = default)
    {
        var products = await repository.GetAllAsync(cancellationToken);
        return mapper.Map<List<ProductDto>>(products);
    }
}
