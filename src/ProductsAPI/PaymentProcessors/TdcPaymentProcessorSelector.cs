using ProductsAPI.PaymentProcessors.Abstractions;
using ProductsAPI.PaymentProcessors.Providers;

namespace ProductsAPI.PaymentProcessors;

public class TdcPaymentProcessorSelector(IEnumerable<TdcFeeProvider> feeProviders, IServiceProvider serviceProvider)
	: ITdcPaymentProcessorSelector
{
	private readonly IEnumerable<TdcFeeProvider> _feeProviders = feeProviders;
	private readonly IServiceProvider _serviceProvider = serviceProvider;

	public IPaymentProcessor? Select(double totalAmount)
	{
		var processorType = GetLowerFeeProcessor(totalAmount);

		if (processorType == null) throw new ArgumentException("Invalid processor type");

		return _serviceProvider.GetService(processorType) as IPaymentProcessor;
	}

	private Type? GetLowerFeeProcessor(double totalAmount)
	{
		var type = _feeProviders
			.Where(x => x.MinAmount < totalAmount)
			.OrderBy(x => x.FeePercent)
			.Select(x => x.ProcessorType)
			.FirstOrDefault();

		return type;
	}
}
