using ProductsAPI.PaymentsGateway.Annotations;

namespace ProductsAPI.PaymentsGateway
{
    public enum PaymentMethod
	{
		[PaymentProcessorType(ProcessorType = typeof(PagaFacilPaymentProcessor))]
		Cash,
		[PaymentProcessorType(ProcessorType = typeof(PaymentProcessor))]
		Card,
		[PaymentProcessorType(ProcessorType = typeof(CazaPagosPaymentProcessor))]
		Transfer
	}
}
