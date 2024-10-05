using ProductsAPI.DataContracts;

namespace ProductsAPI.AppServices
{
	public interface IProductsAppService
	{
		Task<IReadOnlyList<ProductResponse>> ListAllAsync();
	}
}
