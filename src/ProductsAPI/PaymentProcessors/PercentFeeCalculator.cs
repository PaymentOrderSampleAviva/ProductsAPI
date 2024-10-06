using Throw;

namespace ProductsAPI.PaymentProcessors;

public class PercentFeeCalculator : FeeCalculator
{
	public PercentFeeCalculator(double minAmount, double maxAmount, double percent)
	: base(minAmount, maxAmount)
	{
		percent.Throw().IfLessThan(0);
		Percent = percent;
	}

	public double Percent { get; init; }

	public override double CalculateFee(double amount)
	{
		return amount * (Percent / 100);
	}
}
