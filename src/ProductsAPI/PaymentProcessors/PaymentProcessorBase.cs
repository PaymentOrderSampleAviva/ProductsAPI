using ProductsAPI.PaymentProcessors.Abstractions;
using ProductsAPI.PaymentProcessors.Model;

namespace ProductsAPI.PaymentProcessors;

public abstract class PaymentProcessorBase : IPaymentProcessor
{
	public abstract Task<OrderCreatedModel> CreateOrderAsync(CreateOrderModel orderModel, CancellationToken cancellationToken = default);
}
