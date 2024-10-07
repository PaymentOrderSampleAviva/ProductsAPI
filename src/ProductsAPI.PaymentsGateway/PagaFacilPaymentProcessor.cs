using Microsoft.Extensions.Logging;

namespace ProductsAPI.PaymentsGateway;

public class PagaFacilPaymentProcessor : ExternalPaymentProcessorBase
{
	public PagaFacilPaymentProcessor(HttpClient httpClient, ILogger<PagaFacilPaymentProcessor> logger)
	: base(httpClient, logger)
	{
	}
}
