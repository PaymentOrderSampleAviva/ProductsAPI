namespace ProductsAPI.PaymentProcessors;

public class CazaPagosPaymentProcessor : ExternalPaymentProcessorBase
{
	public CazaPagosPaymentProcessor(HttpClient httpClient, ILogger<CazaPagosPaymentProcessor> logger) 
		: base(httpClient, logger)
	{
	}

	protected override string TranslatePaymentMethod(string method)
	{
		if (method.ToLower().Equals("card")) return "CreditCard";

		return method;
	}
}
