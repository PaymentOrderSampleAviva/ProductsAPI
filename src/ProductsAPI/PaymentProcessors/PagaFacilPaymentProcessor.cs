namespace ProductsAPI.PaymentProcessors;

public class PagaFacilPaymentProcessor : ExternalPaymentProcessorBase
{
	public PagaFacilPaymentProcessor(HttpClient httpClient, ILogger<PagaFacilPaymentProcessor> logger)
	: base(httpClient, logger)
	{
	}
}
