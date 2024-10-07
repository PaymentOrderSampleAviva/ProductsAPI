using ProductsAPI.PaymentProcessors.Abstractions;
using ProductsAPI.PaymentProcessors.Model;
using Throw;

namespace ProductsAPI.PaymentProcessors;

public class PaymentProcessor(IPaymentProcessorSelector processorSelector) : PaymentProcessorBase
{
	public override async Task<OrderCreatedModel> CreateOrderAsync(CreateOrderModel orderModel, CancellationToken cancellationToken = default)
	{
		orderModel.ThrowIfNull(nameof(orderModel));
		orderModel.Products.Throw().IfCountLessThan(1);

		var paymentProcessor = processorSelector.Select(orderModel.GetTotalAmount());

		if (paymentProcessor == null) throw new ArgumentNullException(nameof(paymentProcessor));

		var response = await paymentProcessor.CreateOrderAsync(orderModel, cancellationToken);
		return await Task.FromResult(response);
	}
}