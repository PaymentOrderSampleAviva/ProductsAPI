using Throw;

namespace ProductsAPI.PaymentProcessors.Providers.Builder;

public class TranserFeesProviderBuilder
{
	private readonly ICollection<FeeProvider> _feedProviders = new List<FeeProvider>();
	private bool defaultFees = false;

	public TranserFeesProviderBuilder WithDefault()
	{
		if (defaultFees) return this;

		_feedProviders.Add(new(0, new FixedFeeCalculator(5)));
		_feedProviders.Add(new(500, new PercentFeeCalculator(2.5)));
		_feedProviders.Add(new(1000, new PercentFeeCalculator(2)));

		defaultFees = true;

		return this;
	}

	public TranserFeesProviderBuilder Add(double minAmount, FeeCalculator feeCalculator)
	{
		minAmount.Throw().IfLessThan(0);
		feeCalculator.ThrowIfNull();

		_feedProviders.Add(new(minAmount, feeCalculator));
		return this;
	}

	public IEnumerable<FeeProvider> Build() => _feedProviders;
}
