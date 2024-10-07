using AutoMapper;
using ProductsAPI.Application.DTOs;
using ProductsAPI.Domain.Repositories;
using ProductsAPI.Application.Abstractions;

namespace ProductsAPI.Application;

public class ProductsApplication(IProductsRepository repository, IMapper mapper) : IProductsApplication
{
    public async Task<IReadOnlyList<ProductDto>> GetProductsAsync(CancellationToken cancellationToken = default)
    {
        var products = await repository.GetAllAsync(cancellationToken);
        return mapper.Map<List<ProductDto>>(products);
    }
}
