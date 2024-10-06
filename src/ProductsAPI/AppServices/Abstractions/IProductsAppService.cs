using ProductsAPI.DataContracts;

namespace ProductsAPI.AppServices.Abstractions
{
    public interface IProductsAppService
    {
        Task<IReadOnlyList<ProductResponse>> ListAllAsync();
    }
}
