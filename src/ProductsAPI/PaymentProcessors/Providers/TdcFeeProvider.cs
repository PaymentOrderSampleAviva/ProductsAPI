using ProductsAPI.PaymentProcessors.Abstractions;
using Throw;

namespace ProductsAPI.PaymentProcessors.Providers;

public class TdcFeeProvider
{
	public TdcFeeProvider(double minAmount, double feePercent, Type processorType)
	{
		minAmount.Throw().IfLessThan(0);
		feePercent.Throw().IfLessThanOrEqualTo(0);
		processorType.ThrowIfNull();

		if (!processorType.IsAssignableTo(typeof(IPaymentProcessor))) throw new ArgumentException($"Processor type must implement {nameof(IPaymentProcessor)} interface.");

		MinAmount = minAmount;
		FeePercent = feePercent;
		ProcessorType = processorType;
	}

	public double MinAmount { get; init; }
	public double FeePercent { get; init; }
	public Type ProcessorType { get; init; }

}
