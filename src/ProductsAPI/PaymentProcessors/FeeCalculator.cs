using Throw;

namespace ProductsAPI.PaymentProcessors;

public abstract class FeeCalculator
{
	public FeeCalculator(double fee)
	{
		fee.Throw().IfLessThan(0);
		Fee = fee;
	}

	public double Fee { get; init; }

	public abstract double CalculateFee(double amount);

}
