using Throw;

namespace ProductsAPI.PaymentProcessors.Providers;

public class FeeProvider
{
	public FeeProvider(double minAmount, FeeCalculator feeCalculator)
	{
		minAmount.Throw().IfLessThan(0);
		feeCalculator.ThrowIfNull();

		MinAmount = minAmount;
		FeeCalculator = feeCalculator;
	}

	public double MinAmount { get; init; }
	public FeeCalculator FeeCalculator { get; init; }

}
