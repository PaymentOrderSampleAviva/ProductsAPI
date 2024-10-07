using Microsoft.EntityFrameworkCore;
using ProductsAPI.AppServices;
using ProductsAPI.AppServices.Abstractions;
using ProductsAPI.Data;
using ProductsAPI.PaymentProcessors;
using ProductsAPI.PaymentProcessors.Abstractions;
using ProductsAPI.PaymentProcessors.Providers;
using ProductsAPI.Repositories;
using System.Reflection;
using Microsoft.Extensions.Http.Resilience;
using Polly;
using System.Net;

namespace ProductsAPI;

public static class DependencyConfig
{
	public static IServiceCollection AddCoreDependenciesDev(this IServiceCollection services)
	{
		services.AddCoreDependencies();
		services.AddInMemoryDataStorage();
		return services;
	}

	public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
	{
		services.AddAppServices();
		services.AddRepositories();
		services.AddPaymentServices();
		services.AddDefaultServices();
		return services;
	}

	private static IServiceCollection AddAppServices(this IServiceCollection services)
	{
		services.AddTransient<IProductsAppService, ProductsAppService>();
		services.AddTransient<IOrdersAppService, OrdersAppService>();
		return services;
	}

	private static IServiceCollection AddRepositories(this IServiceCollection services)
	{
		services.AddTransient<IProductsRepository, ProductsRepository>();
		services.AddTransient<IOrdersRepository, OrdersRepository>();
		return services;
	}

	private static IServiceCollection AddInMemoryDataStorage(this IServiceCollection services)
	{
		services.AddDbContext<ApplicationDbContext>(options =>
		{
			options.UseInMemoryDatabase("ProductsDb-Dev");
		});
		return services;
	}

	private static IServiceCollection AddPaymentServices(this IServiceCollection services)
	{
		services.AddSingleton<IPaymentMethodSelector, PaymentMethodSelector>();
		services.AddSingleton<IPaymentProcessorSelector, PaymentProcessorSelector>();
		services.AddSingleton<PaymentProcessor>();

		services.AddHttpClient<PagaFacilPaymentProcessor>(
		client =>
		{
			// Set the base address of the named client.
			client.BaseAddress = new Uri("https://app-paga-chg-aviva.azurewebsites.net/");

			// Add a user-agent default request header.
			client.DefaultRequestHeaders.Add("x-api-key", "apikey-fj9esodija09s2");
		}).AddStandardResilienceHandler();

		services.AddHttpClient<CazaPagosPaymentProcessor>(
		client =>
		{
			// Set the base address of the named client.
			client.BaseAddress = new Uri("https://app-caza-chg-aviva.azurewebsites.net/");

			// Add a user-agent default request header.
			client.DefaultRequestHeaders.Add("x-api-key", "apikey-fj9esodija09s2");
		}).AddResilienceHandler("CustomPipeline", static builder => GetHttpResiliencePolicies(builder));

		// Card Fee providers
		services.AddSingleton(new FeeProvider (minAmount: 0, feePercent: 1, processorType: typeof(PagaFacilPaymentProcessor)));
		services.AddSingleton(new FeeProvider (minAmount: 0, feePercent: 2, processorType: typeof(CazaPagosPaymentProcessor)));
		services.AddSingleton(new FeeProvider (minAmount: 1500, feePercent: 1.5, processorType: typeof(CazaPagosPaymentProcessor)));
		services.AddSingleton(new FeeProvider (minAmount: 5000, feePercent: 0.5, processorType: typeof(CazaPagosPaymentProcessor)));

		return services;
	}

	private static IServiceCollection AddDefaultServices(this IServiceCollection services)
	{
		services.AddAutoMapper(Assembly.GetExecutingAssembly());
		services.AddLogging();
		return services;
	}

	private static ResiliencePipelineBuilder<HttpResponseMessage> GetHttpResiliencePolicies(ResiliencePipelineBuilder<HttpResponseMessage> builder)
	{
		// See: https://www.pollydocs.org/strategies/retry.html
		builder.AddRetry(new HttpRetryStrategyOptions
		{
			// Customize and configure the retry logic.
			BackoffType = DelayBackoffType.Exponential,
			MaxRetryAttempts = 5,
			UseJitter = true
		});

		builder.AddCircuitBreaker(new HttpCircuitBreakerStrategyOptions
		{
			// Customize and configure the circuit breaker logic.
			SamplingDuration = TimeSpan.FromSeconds(10),
			FailureRatio = 0.2,
			MinimumThroughput = 3,
			ShouldHandle = static args =>
			{
				return ValueTask.FromResult(args is
				{
					Outcome.Result.StatusCode:
						HttpStatusCode.RequestTimeout or HttpStatusCode.TooManyRequests
				});
			}
		});

		// See: https://www.pollydocs.org/strategies/timeout.html
		builder.AddTimeout(TimeSpan.FromSeconds(5));

		return builder;
	}
}
