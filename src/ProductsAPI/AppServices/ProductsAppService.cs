using AutoMapper;
using ProductsAPI.DataContracts;
using ProductsAPI.Repositories;

namespace ProductsAPI.AppServices;

public class ProductsAppService(IProductsRepository repository, IMapper mapper) : IProductsAppService 
{
	private readonly IProductsRepository _repository = repository;
	private readonly IMapper _mapper = mapper;

	public async Task<IReadOnlyList<ProductResponse>> ListAllAsync()
	{
		var products = await _repository.GetAllAsync();
		return _mapper.Map<List<ProductResponse>>(products);
	}
}
