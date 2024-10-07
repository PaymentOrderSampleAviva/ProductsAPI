using ProductsAPI.PaymentsGateway.Abstractions;
using ProductsAPI.PaymentsGateway.Model;

namespace ProductsAPI.PaymentsGateway;

public abstract class PaymentProcessorBase : IPaymentProcessor
{
	public abstract Task<OrderCreatedModel> CreateOrderAsync(CreateOrderModel orderModel, CancellationToken cancellationToken = default);
}
