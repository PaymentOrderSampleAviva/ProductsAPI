using Throw;

namespace ProductsAPI.PaymentProcessors
{
	public class FixedFeeCalculator : FeeCalculator
	{
        public FixedFeeCalculator(double minAmount, double maxAmount, double fixedFee)
            : base(minAmount, maxAmount)
		{
			fixedFee.Throw().IfLessThan(0);
            FixedFee = fixedFee;
        }

        public double FixedFee { get; init; }

        public override double CalculateFee(double amount)
		{
			return FixedFee;
		}
	}
}
