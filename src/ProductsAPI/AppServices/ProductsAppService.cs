using AutoMapper;
using ProductsAPI.AppServices.Abstractions;
using ProductsAPI.DTOs;
using ProductsAPI.Repositories;

namespace ProductsAPI.AppServices;

public class ProductsAppService(IProductsRepository repository, IMapper mapper) : IProductsAppService 
{
	public async Task<IReadOnlyList<ProductDto>> GetProductsAsync(CancellationToken cancellationToken = default)
	{
		var products = await repository.GetAllAsync(cancellationToken);
		return mapper.Map<List<ProductDto>>(products);
	}
}
