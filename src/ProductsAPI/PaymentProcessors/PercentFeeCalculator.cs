namespace ProductsAPI.PaymentProcessors;

public class PercentFeeCalculator : FeeCalculator
{
	public PercentFeeCalculator(double fee)
		: base(fee)
	{

	}

	public override double CalculateFee(double amount)
	{
		return amount * (Fee / 100);
	}
}
