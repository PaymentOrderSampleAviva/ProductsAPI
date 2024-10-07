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
using ProductsAPI.PaymentProcessors.Options;
using System.Text;

namespace ProductsAPI;

public static class DependencyConfig
{
	public static IServiceCollection AddCoreDependenciesDev(this IServiceCollection services, ConfigurationManager configuration)
	{
		services.AddCoreDependencies(configuration);
		services.AddInMemoryDataStorage();
		return services;
	}

	public static IServiceCollection AddCoreDependencies(this IServiceCollection services, ConfigurationManager configuration)
	{
		services.AddAppServices();
		services.AddRepositories();
		services.AddPaymentServices(configuration);
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

	private static IServiceCollection AddPaymentServices(this IServiceCollection services, ConfigurationManager configuration)
	{
		services.AddSingleton<IPaymentMethodSelector, PaymentMethodSelector>();
		services.AddSingleton<IPaymentProcessorSelector, PaymentProcessorSelector>();
		services.AddSingleton<PaymentProcessor>();

		services.AddHttpClient<PagaFacilPaymentProcessor>(
		client =>
		{
			var options = configuration.GetSection(PaymentProcessorOptions.PagaFacil)
													 .Get<PaymentProcessorOptions>();

			if (options == null) throw new ArgumentNullException(nameof(options));

			// Set the base address of the named client.
			client.BaseAddress = new Uri(options.BaseUrl);

			// Add a user-agent default request header.
			client.DefaultRequestHeaders.Add("x-api-key", options.ApiKey);
		}).AddStandardResilienceHandler();

		services.AddHttpClient<CazaPagosPaymentProcessor>(
		client =>
		{
			var options = configuration.GetSection(PaymentProcessorOptions.CazaPagos)
													 .Get<PaymentProcessorOptions>();

			if (options == null) throw new ArgumentNullException(nameof(options));

			// Set the base address of the named client.
			client.BaseAddress = new Uri(options.BaseUrl);

			// Add a user-agent default request header.
			client.DefaultRequestHeaders.Add("x-api-key", options.ApiKey);
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
