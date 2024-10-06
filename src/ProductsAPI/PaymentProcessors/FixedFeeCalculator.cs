namespace ProductsAPI.PaymentProcessors;

public class FixedFeeCalculator : FeeCalculator
{
	public FixedFeeCalculator(double fee)
	: base(fee)
	{

	}

	public override double CalculateFee(double amount)
	{
		return Fee;
	}
}
