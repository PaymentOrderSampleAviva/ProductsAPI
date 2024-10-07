using Microsoft.Extensions.DependencyInjection;
using ProductsAPI.Application.Abstractions;
using System.Reflection;

namespace ProductsAPI.Application;

public static class DependencyConfig
{
	public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
	{
		services.AddTransient<IProductsApplication, ProductsApplication>();
		services.AddTransient<IOrdersApplication, OrdersApplication>();
		services.AddAutoMapper(Assembly.GetExecutingAssembly());

		return services;
	}
}
