using ProductsAPI.PaymentProcessors.Model;
using Throw;

namespace ProductsAPI.PaymentProcessors;

public class CashPaymentProcessor (ILogger<CashPaymentProcessor> logger) : PaymentProcessorBase
{
	private const int TRANSACTION_FEE = 15;
	private readonly ILogger _logger = logger;

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
			Fees = new List<FeeModel>(GetTransactionFees())
		};

		_logger.LogInformation($"Cash transaction completed for order id: {response.OrderId}, total fee: {response.Fees.Sum(x => x.Amount)}");
		return await Task.FromResult(response);
	}

	private IReadOnlyList<FeeModel> GetTransactionFees()
	{
		return new List<FeeModel>
		{
			new FeeModel { Name = "Transaction fee", Amount = TRANSACTION_FEE }
		};
	}
}
