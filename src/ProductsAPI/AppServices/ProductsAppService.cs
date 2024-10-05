using AutoMapper;
using ProductsAPI.DataContracts;
using ProductsAPI.Repositories;

namespace ProductsAPI.AppServices;

public class ProductsAppService(IProductsRepository repository, IMapper mapper, Logger<ProductsAppService> logger) : IProductsAppService 
{
	private readonly IProductsRepository _repository = repository;
	private readonly IMapper _mapper = mapper;
	private readonly ILogger _logger = logger;

	public async Task<IReadOnlyList<ProductResponse>> ListAllAsync()
	{
		var products = await _repository.GetAllAsync();
		return _mapper.Map<List<ProductResponse>>(products);
	}
}
