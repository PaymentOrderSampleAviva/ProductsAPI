using ProductsAPI.Annotations;

namespace ProductsAPI.PaymentProcessors
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
