using Throw;

namespace ProductsAPI.PaymentProcessors;

public abstract class FeeCalculator
{
	protected FeeCalculator(double minAmount, double maxAmount)
	{
		minAmount.Throw().IfGreaterThan(maxAmount);

		MinAmount = minAmount;
		MaxAmount = maxAmount;
	}

	public double MinAmount { get; init; }
	public double MaxAmount { get; init; }

	public abstract double CalculateFee(double amount);

}
