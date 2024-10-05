using Microsoft.EntityFrameworkCore;
using ProductsAPI.AppServices;
using ProductsAPI.Data;
using ProductsAPI.Repositories;
using System.Reflection;

namespace ProductsAPI;

public static class DependencyConfig
{
	public static IServiceCollection AddCoreDependenciesDev(this IServiceCollection services)
	{
		services.AddCoreDependencies();
		services.AddInMemoryDataStorage();
		return services;
	}

	public static IServiceCollection AddCoreDependenciesPro(this IServiceCollection services)
	{
		services.AddCoreDependencies();
		//Register production data storage here.
		return services;
	}

	public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
	{
		services.AddAppServices();
		services.AddRepositories();
		services.AddDefaultServices();
		return services;
	}

	private static IServiceCollection AddAppServices(this IServiceCollection services)
	{
		services.AddTransient<IProductsAppService, ProductsAppService>();
		return services;
	}

	private static IServiceCollection AddRepositories(this IServiceCollection services)
	{
		services.AddTransient<IProductsRepository, ProductsRepository>();
		return services;
	}

	private static IServiceCollection AddInMemoryDataStorage(this IServiceCollection services)
	{
		services.AddDbContext<ApplicationDbContext>(options => {
			options.UseInMemoryDatabase("ProductsDb-Dev");
		});
		return services;
	}

	private static IServiceCollection AddDefaultServices(this IServiceCollection services)
	{
		services.AddAutoMapper(Assembly.GetExecutingAssembly());
		services.AddLogging();
		return services;
	}
}
