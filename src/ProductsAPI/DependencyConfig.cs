using ProductsAPI.Data;
using ProductsAPI.Application;

namespace ProductsAPI;

public static class DependencyConfig
{
	public static IServiceCollection AddCoreDependenciesDev(this IServiceCollection services, ConfigurationManager configuration)
	{
		services.AddApplicationLayer();
		services.AddDataLayer();
		services.AddPaymentServices(configuration);
		return services;
	}
}
