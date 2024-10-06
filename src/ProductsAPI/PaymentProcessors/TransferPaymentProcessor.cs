using ProductsAPI.PaymentProcessors.Model;
using ProductsAPI.PaymentProcessors.Providers;
using ProductsAPI.PaymentProcessors.Providers.Builder;
using Throw;

namespace ProductsAPI.PaymentProcessors;

public class TransferPaymentProcessor : PaymentProcessorBase
{
	private readonly ILogger _logger;
	private readonly IEnumerable<FeeProvider> _feedProviders;

    public TransferPaymentProcessor(ILogger<CashPaymentProcessor> logger, IEnumerable<FeeProvider> feeProviders)
	{
		_logger = logger;
		_feedProviders = feeProviders;
	}

    public TransferPaymentProcessor(ILogger<CashPaymentProcessor> logger)
		: this(logger, new TranserFeesProviderBuilder().WithDefault().Build())
	{
		
	}

	public override async Task<OrderCreatedModel> CreateOrderAsync(CreateOrderModel orderModel)
	{
		try
		{
			orderModel.ThrowIfNull();
			orderModel.Products.Throw().IfCountLessThan(1);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Payment can't be processed due to an exception, check the exception object for more info.");
			throw new ProcessPaymentValidationException("A validation exception accours", ex);
		}

		_logger.LogInformation($"Cash transaction started, total amount: {orderModel.Products.Sum(x => x.UnitPrice)}");

		var response = new OrderCreatedModel
		{
			OrderId = Guid.NewGuid(),
			Products = new List<ProductModel>(orderModel.Products),
			Fees = new List<FeeModel>(GetTransactionFees(orderModel.GetTotalAmount()))
		};

		_logger.LogInformation($"Cash transaction completed for order id: {response.OrderId}, total fee: {response.Fees.Sum(x => x.Amount)}");
		return await Task.FromResult(response);
	}

	private IReadOnlyList<FeeModel> GetTransactionFees(double totalAmount)
	{
		var fees = new List<FeeModel>();
		var feeCalculator = GetFeeCalculator(totalAmount);
		
		if (feeCalculator != null)
		{
			_logger.LogInformation($"Calculating fee with: {feeCalculator.GetType().Name} for amount: {totalAmount}");
			fees.Add(new FeeModel { Name = "Transfer fee", Amount = feeCalculator.CalculateFee(totalAmount) });
		}
		else
		{
			_logger.LogError($"No fee calculator was found for amount: {totalAmount}");
		}

		return fees;
	}

	private FeeCalculator? GetFeeCalculator(double totalAmount)
	{
		var provider = _feedProviders
			.Where(p => p.MinAmount < totalAmount)
			.OrderByDescending(p => p.MinAmount)
			.FirstOrDefault();

		return provider?.FeeCalculator;
	}
}