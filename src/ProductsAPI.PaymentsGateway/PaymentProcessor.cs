using ProductsAPI.PaymentsGateway.Abstractions;
using ProductsAPI.PaymentsGateway.Model;
using Throw;

namespace ProductsAPI.PaymentsGateway;

public class PaymentProcessor(IPaymentProcessorSelector processorSelector) : PaymentProcessorBase
{
	private readonly IPaymentProcessorSelector _processorSelector = processorSelector;

	public override async Task<OrderCreatedModel> CreateOrderAsync(CreateOrderModel orderModel, CancellationToken cancellationToken = default)
	{
		orderModel.ThrowIfNull(nameof(orderModel));
		orderModel.Products.Throw().IfCountLessThan(1);

		var paymentProcessor = _processorSelector.Select(orderModel.GetTotalAmount());

		if (paymentProcessor == null) throw new ArgumentNullException(nameof(paymentProcessor));

		var response = await paymentProcessor.CreateOrderAsync(orderModel, cancellationToken);
		return await Task.FromResult(response);
	}
}