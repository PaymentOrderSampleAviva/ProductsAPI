using AutoMapper;
using ProductsAPI.Application.DTOs;
using ProductsAPI.Domain.Repositories;
using ProductsAPI.Application.Abstractions;

namespace ProductsAPI.Application;

public class ProductsAppService(IProductsRepository repository, IMapper mapper) : IProductsAppService
{
    private readonly IProductsRepository _repository = repository;
    private readonly IMapper _mapper = mapper;

    public async Task<IReadOnlyList<ProductDto>> GetProductsAsync(CancellationToken cancellationToken = default)
    {
        var products = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map<List<ProductDto>>(products);
    }
}
