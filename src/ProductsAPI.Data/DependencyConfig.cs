using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductsAPI.Data.Repositories;
using ProductsAPI.Domain.Repositories;

namespace ProductsAPI.Data;

public static class DependencyConfig
{
	public static IServiceCollection AddDataLayer(this IServiceCollection services)
	{
		services.AddTransient<IProductsRepository, ProductsRepository>();
		services.AddTransient<IOrdersRepository, OrdersRepository>();

		services.AddDbContext<ApplicationDbContext>(options =>
		{
			options.UseInMemoryDatabase("ProductsDb-Dev");
		});

		return services;
	}
}
