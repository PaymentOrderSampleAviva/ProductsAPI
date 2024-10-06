using ProductsAPI.DataContracts;
using ProductsAPI.Extensions;
using ProductsAPI.PaymentProcessors;
using ProductsAPI.PaymentProcessors.Abstractions;
using Throw;

namespace ProductsAPI.AppServices;

public class OrderAppService (IServiceProvider serviceProvider) : IOrderAppService
{
	private readonly IServiceProvider _serviceProvider = serviceProvider;

	public async Task<object> CreateOrderAsync(CreateOrderRequest request)
	{
		request.ThrowIfNull();
		request.Products.Throw().IfCountLessThan(1);

		var paymentProcessor = GetPaymentProcessor(request.Method);

		throw new NotImplementedException();
	}

	private IPaymentProcessor? GetPaymentProcessor(PaymentMethod method)
	{
		var type = method.GetPaymentProcessorType();
		type.ThrowIfNull();

		return _serviceProvider.GetService(type) as IPaymentProcessor;
		
	}
}
